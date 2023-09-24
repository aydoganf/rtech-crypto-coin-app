using System.Text.Json.Serialization;

namespace RTech.CryptoCoin.Web.Workers;

public class CryptoCurrencyResponse
{
    [JsonPropertyName("status")]
    public CryptoCurrencyResponseStatus Status { get; set; }

    [JsonPropertyName("data")]
    public List<CryptoCurrencyResponseData> Data { get; set; }
}

public class CryptoCurrencyResponseStatus
{
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("error_code")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("error_message")]
    public string ErrorMessage { get; set; }
}

public class CryptoCurrencyResponseData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("quote")]
    public Dictionary<string, CryptoCurrencyResponseDataDetail> Quote { get; set; }
}

public class CryptoCurrencyResponseDataDetail
{
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("last_updated")]
    public DateTime LastUpdated { get; set; }
}