using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Services
{
    public class Class1 : IFinnhubService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public Class1(HttpClient httpClient, IConfiguration configuration)

        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

       //1. get token from config
       //2. Build URL
       //3. Call url 
       //4. Read body as string - not stream
       //5. Deserialize + return
    

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
          if(string.IsNullOrEmpty(stockSymbol)){
                throw new ArgumentException("Stock symbol cannot be empty", nameof(stockSymbol));
            }

            var token = _configuration["Finnhub:Token"];
            var url = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={token}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Finnhub API call failed with status code:  {response.StatusCode}");
            }
          string responseContent  = await  response.Content.ReadAsStringAsync();
            var deserialize = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);

            return deserialize;

        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            if (string.IsNullOrEmpty(stockSymbol)) 
            {
                throw new ArgumentException("Stock symbol can not be null", nameof(stockSymbol));
            }
            
            var token = _configuration["Finnhub:Token"];
            var url = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {

            }
            string body = await response.Content.ReadAsStringAsync();
            var content = JsonSerializer.Deserialize<Dictionary<string, object>>(body);
            return content;
        }
    }
}
