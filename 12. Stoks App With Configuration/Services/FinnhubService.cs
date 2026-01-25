using System.Text.Json;
using ServiceContracts;

namespace Services;

public class FinnhubService : IFinnhubService
{
   private readonly IHttpClientFactory _httpClientFactory;

   public FinnhubService(IHttpClientFactory httpClientFactory)
   {
      _httpClientFactory = httpClientFactory;
   }

   public async Task<Dictionary<string, object?>> GetStockPriceQuote(string stockSymbol)
   {
      using (HttpClient httpClient = _httpClientFactory.CreateClient())
      {
         HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
         {
            RequestUri = new Uri("https://finnhub.io/api/v1/quote?symbol=MSFT&token=d5r3eohr01qqqlh9irn0d5r3eohr01qqqlh9irng"),
            Method = HttpMethod.Get,
         };

         HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
         
         Stream stream = httpResponseMessage.Content.ReadAsStream();
         StreamReader streamReader = new StreamReader(stream);
         string response = streamReader.ReadToEnd();
         Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

         if (response == null)
         {
            throw new InvalidOperationException("No response from finnhub server");
         }

         if (responseDictionary.ContainsKey("error"))
         {
            throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
         }
         
         return responseDictionary;
      }
   }
}