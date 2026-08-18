using Dtos;
using Models;
using System.ComponentModel.DataAnnotations;
namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrder;

        public StocksService()
        {
            _buyOrders =new List<BuyOrder>();
            _sellOrder = new List<SellOrder>();
        }
        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if(buyOrderRequest  == null)
            { 
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }

            //validation for other data anotations
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(buyOrderRequest);

            bool isValid = Validator.TryValidateObject(buyOrderRequest, validationContext, validationResults, true);
            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }

            //convert DTO => ENTITY

            BuyOrder buyOrder = new BuyOrder()
            {
                BuyOrderID = Guid.NewGuid(),
                StockName = buyOrderRequest.StockName,
                StockSymbol = buyOrderRequest.StockSymbol,
                DateAndTimeOfOrder = buyOrderRequest.DateAndTimeOfOrder,
                Price = buyOrderRequest.Price,
                Quantity = buyOrderRequest.Quantity
                
            };

            //add entity  to storage from above 
            _buyOrders.Add(buyOrder);


            //convert from Entity => responseDTO 
            BuyOrderResponse buyOrderResponse = new BuyOrderResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockName = buyOrder.StockName,
                StockSymbol = buyOrder.StockSymbol,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Price = buyOrder.Price,
                Quantity = buyOrder.Quantity,
                TradeAmount = buyOrder.Price * buyOrder.Quantity
            };

            return buyOrderResponse;
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
           if(sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }

            //store list of exceptions 
            List<ValidationResult> validationResult = new List<ValidationResult>(); //initialize list
            ValidationContext validationContext = new ValidationContext(sellOrderRequest);//what to validate

            bool isValid = Validator.TryValidateObject(sellOrderRequest, validationContext, validationResult, true);
            if (!isValid)
            {
                throw new ArgumentException(validationResult.FirstOrDefault()?.ErrorMessage);
            }

            //convert dto => entity
            SellOrder sellOrder = new SellOrder()
            {
                SellOrderID = Guid.NewGuid(),
                StockName = sellOrderRequest.StockName,
                StockSymbol = sellOrderRequest.StockSymbol,
                DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
                Price = sellOrderRequest.Price,
                Quantity = sellOrderRequest.Quantity
            };

            _sellOrder.Add(sellOrder);

            SellOrderResponse sellOrderResponse = new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockName = sellOrder.StockName,
                StockSymbol = sellOrder.StockSymbol,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Price = sellOrder.Price,
                Quantity = sellOrder.Quantity,
                TradeAmount = sellOrder.Price * sellOrder.Quantity
            };

            return sellOrderResponse;
        }

        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            // no validation - no entity creation - 
            //1. take existing _buyOrders a list<BuyOrder> entities 
            //2. convert each one into a BuyOrderResponse - interface returncs List<BuyOrderResponse> no raw entities-
            //** controllers should never see entities directly. 

           List<BuyOrderResponse> buyOrderRespose = _buyOrders.Select(buyOrder => new BuyOrderResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockName = buyOrder.StockName,
                StockSymbol = buyOrder.StockSymbol,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Price = buyOrder.Price,
                Quantity = buyOrder.Quantity,
                TradeAmount = buyOrder.Price * buyOrder.Quantity
            }).ToList();

            return Task.FromResult(buyOrderRespose);
        }
        
        

        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> sellOrderResponse = _sellOrder.Select(sellOrder => new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockName = sellOrder.StockName,
                StockSymbol = sellOrder.StockSymbol,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Price = sellOrder.Price,
                Quantity = sellOrder.Quantity,
                TradeAmount = sellOrder.Price * sellOrder.Quantity
            }).ToList();

            return Task.FromResult(sellOrderResponse);
        }
    }
}
