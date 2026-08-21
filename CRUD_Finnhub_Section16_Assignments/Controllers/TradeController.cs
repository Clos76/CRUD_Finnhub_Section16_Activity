using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using ViewModels;


using Microsoft.Extensions.Options;
using Dtos;

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
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            if (ModelState.IsValid)
            {
                var buyOrder = await _stocksService.CreateBuyOrder(buyOrderRequest);
                return RedirectToAction("Orders");
            }
            else
            {

                StockTrade stockTrade = new StockTrade()
                {
                    StockName = buyOrderRequest.StockName,
                    StockSymbol = buyOrderRequest.StockSymbol,
                    Price = buyOrderRequest.Price,
                    Quantity = buyOrderRequest.Quantity,
                };

                return View("Index",stockTrade);
            }

           
        }

        [HttpPost("SellOrder")]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            //need update date 
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            if (ModelState.IsValid)
            {
              var sellOrder= await  _stocksService.CreateSellOrder(sellOrderRequest);
              return RedirectToAction("Orders");
            }
            else
            {
                StockTrade sellTrade = new StockTrade()
                {
                    StockName = sellOrderRequest.StockName,
                    StockSymbol = sellOrderRequest.StockSymbol,
                    Price = sellOrderRequest.Price,
                    Quantity = sellOrderRequest.Quantity
                };
                return View("Index", sellTrade);
            }

           
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
