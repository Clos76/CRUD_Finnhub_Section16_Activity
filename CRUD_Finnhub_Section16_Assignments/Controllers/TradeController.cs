using Microsoft.AspNetCore.Mvc;
using Models;
using Services;


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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BuyOrder()
        {
            return View();
        }

        public IActionResult SellOrder()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }
    }
}
