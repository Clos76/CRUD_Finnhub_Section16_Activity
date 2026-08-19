using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using ViewModels;


using Microsoft.Extensions.Options;

namespace CRUD_Finnhub_Section16_Assignments.Controllers
{
    [Route("Trade")]
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IStocksService _stocksService;
        private readonly IOptions<TradingOptions> _options;

        public TradeController(IFinnhubService finnhubService, IStocksService stocksService, IOptions<TradingOptions> options)
        {
            _finnhubService = finnhubService;
            _stocksService = stocksService;
            _options = options;
        }


        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
           var defaultStockSymbol =  _options.Value.DefaultStockSymbol;
            var defaultQuantity = _options.Value.DefaultOrderQuantity;
            var stockProfile = await _finnhubService.GetCompanyProfile(defaultStockSymbol);
            var stockQuote = await _finnhubService.GetStockPriceQuote(defaultStockSymbol);
            StockTrade stockTrade = new StockTrade()
            {
                StockName = stockProfile["name"].ToString(),
                StockSymbol = stockProfile["ticker"].ToString(),
                Price = Convert.ToDouble(stockQuote["c"]),
                Quantity = defaultQuantity

            }; 
            return View(stockTrade);
        }

        [HttpPost("BuyOrder")]
        public IActionResult BuyOrder()
        {
            return View();
        }

        [HttpPost("SellOrder")]
        public IActionResult SellOrder()
        {
            return View();
        }

        [HttpGet("Orders")]
        public async Task<IActionResult> Orders()
        {

         var buyOrders =  await  _stocksService.GetBuyOrders();
         var sellOrders = await  _stocksService.GetSellOrders();

            //use our  ViewModels  -- Order--  then convert our list above to  ViewModel;
            Orders ordersViewModel = new Orders()
            {
                BuyOrders = buyOrders,
                SellOrders = sellOrders
            };
            return View(ordersViewModel);
        }
    }
}
