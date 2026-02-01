using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceContracts;

namespace Services;

public class FinnhubService : IFinnhubService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }
    public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
    {
        // Create http client
        HttpClient httpClient = _httpClientFactory.CreateClient();
        
        // Create http request
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
        };
        
        // Send http request and get response
        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        
        // Read response body
        string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();
        
        // Convert response body (from JSON into Dictionary)
        Dictionary<string, object>? responseDictionary =
            JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

        if (responseDictionary == null)
            throw new InvalidOperationException("No pesponse from server");

        if (responseDictionary.ContainsKey("error"))
            throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
        
        // Return response dictionary back to the caller
        return responseDictionary;
    }

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
        // Create http client
        HttpClient httpClient = _httpClientFactory.CreateClient();
        
        // Create http request
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}") //URI includes the secret token
        };
        
        // Send request
        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        
        // Read response body
        string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();
        
        // Convert response body (from JSON into Dictionary)
        Dictionary<string, object>? responseDictionary =
            JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

        if (responseDictionary == null)
            throw new InvalidOperationException("No response from server");

        if (responseDictionary.ContainsKey("error"))
            throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

        return responseDictionary;
    }
}