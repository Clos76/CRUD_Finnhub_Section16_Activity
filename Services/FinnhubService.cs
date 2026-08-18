using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        

        public FinnhubService(HttpClient httpClient, IConfiguration configuration)
        {  
            _httpClient = httpClient; 
            _configuration = configuration;
        }


        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
          if(string.IsNullOrEmpty(stockSymbol))
                {
                throw new ArgumentNullException(nameof(stockSymbol), "Stock Symbol cant be null or empty");
            }

          

            var token = _configuration["Finnhub:Token"];
            var url = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}";

          HttpResponseMessage response =  await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Finnhub API call failed with status code: {response.StatusCode}");
            }
         string responseContent =  await  response.Content.ReadAsStringAsync();
           
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);
            
            return deserialized; 
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            if (string.IsNullOrEmpty(stockSymbol))
            {
                throw new ArgumentNullException(nameof(stockSymbol),"Stock symbol cant be null or empty");
            }
            var token = _configuration["Finnhub:Token"];
            string url = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}";

           HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Finnhub API call failed with status code: {response.StatusCode}");
            }
            string responseContent =  await response.Content.ReadAsStringAsync();
            var deserialized = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);
            return deserialized;
        }
    }
}
