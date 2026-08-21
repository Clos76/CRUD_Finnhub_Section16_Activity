using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Services;
using ViewModels;

namespace CRUD_Finnhub_Section16_Assignments.Controllers
{
    [Route("test")]
    public class HomeController : Controller
    {
        private IFinnhubService _finnhubService;
        private IStocksService _stockService;
        private readonly IConfiguration _configuration;
        private readonly IOptions<TradingOptions> _options;

        public HomeController(IFinnhubService finhubservice, IStocksService stocksService, IConfiguration config)
        {
            _finnhubService = finhubservice;
            _stockService = stocksService;
            _configuration = config;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
          var defaultStockSymbol=  _options.Value.DefaultStockSymbol;
            var defaultStockQuantity = _options.Value.DefaultOrderQuantity;
            var stockProfile = await _finnhubService.GetCompanyProfile(defaultStockSymbol);
            var stockQuote =await _finnhubService.GetStockPriceQuote(defaultStockSymbol);

            StockTrade stockTrade = new StockTrade()
            {
                StockName = stockProfile["name"].ToString(),
                StockSymbol = stockProfile["ticker"].ToString(),
                Price = Convert.ToDouble(stockQuote["c"]),
                Quantity = defaultStockQuantity,
                
            };



            return View(stockTrade);
        }

        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            //get sell and buy orders
          var sellOrders= await  _stockService.GetSellOrders();
          var buyOrders=await  _stockService.GetBuyOrders();

            Orders ordersTogether = new Orders()
            {
                BuyOrders = buyOrders,
                SellOrders = sellOrders,
            };


            return View(ordersTogether);
        }
    }
}
