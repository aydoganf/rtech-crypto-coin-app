using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RTech.CryptoCoin.Coins;

namespace RTech.CryptoCoin.Web.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBitcoinRepository _bitcoinRepository;

    public IndexModel(ILogger<IndexModel> logger, IBitcoinRepository bitcoinRepository)
    {
        _logger = logger;
        _bitcoinRepository = bitcoinRepository;
    }

    public void OnGet()
    {

    }

    protected async Task<JsonResult> GetChartData(DateTime beginDate, DateTime endDate)
    {
        var bitconValues = (await _bitcoinRepository
            .GetListAsync(b => b.CreationTime >= beginDate && b.CreationTime <= endDate)
            ).OrderBy(b => b.CreationTime);

        return new JsonResult(new
        {
            xAxis = bitconValues.Select(b => b.CreationTime.ToString("dd.MM.yyyy HH:mm:ss")).ToArray(),
            yAxis = bitconValues.Select(b => b.Value).ToArray(),
            yMin = bitconValues.Min(b => b.Value),
            yMax = bitconValues.Max(b => b.Value),
        });
    }

    public async Task<JsonResult> OnGetDailyChartData() => await GetChartData(DateTime.Now.AddDays(-1), DateTime.Now);

    public async Task<JsonResult> OnGetMonthChartData() => await GetChartData(DateTime.Now.AddMonths(-1), DateTime.Now);

    public async Task<JsonResult> OnGetYearChartData() => await GetChartData(DateTime.Now.AddYears(-1), DateTime.Now);
}