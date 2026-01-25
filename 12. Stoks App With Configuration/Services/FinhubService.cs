namespace Services;

public class FinhubService
{
   private readonly IHttpClientFactory _httpClientFactory;

   public FinhubService(IHttpClientFactory httpClientFactory)
   {
      _httpClientFactory = httpClientFactory;
   }
}