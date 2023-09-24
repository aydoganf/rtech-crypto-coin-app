using Microsoft.Extensions.DependencyInjection;
using RTech.CryptoCoin.Coins;
using RTech.CryptoCoin.UnitOfWork;
using System.Text.Json;

namespace RTech.CryptoCoin.Web.Workers;

public class CryptoCurrencyWorker : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private IHttpClientFactory _httpClientFactory;
    private IConfiguration _configuration;
    private IBitcoinRepository _bitcoinRepository;
    private Timer? _timer = null;

    public CryptoCurrencyWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));

        await Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            _httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
            _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            _bitcoinRepository = scope.ServiceProvider.GetRequiredService<IBitcoinRepository>();

            var httpClient = _httpClientFactory.CreateClient();

            var httpMessage = new HttpRequestMessage(HttpMethod.Get, _configuration["CoinMarketAPI:Listing:Url"])
            {
            };

            httpMessage.Headers.Add("X-CMC_PRO_API_KEY", _configuration["CoinMarketAPI:ApiKey"]);

            var httpResponse = httpClient.SendAsync(httpMessage).GetAwaiter().GetResult();
            if (httpResponse.IsSuccessStatusCode)
            {
                var strResponse = httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var response = JsonSerializer.Deserialize<CryptoCurrencyResponse>(strResponse);

                if (response.Status.ErrorCode == 0)
                {
                    var uow = scope.ServiceProvider
                        .GetRequiredService<IUnitOfWorkManager>()
                        .Create();

                    _bitcoinRepository.InsertAsync(new Bitcoin(Guid.NewGuid(), response.Data.First(d => d.Symbol == "BTC").Quote["USD"].Price))
                        .GetAwaiter()
                        .GetResult();

                    uow.CompleteAsync().GetAwaiter().GetResult();
                }
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer = null;

        GC.SuppressFinalize(this);
    }
}
