
using Dtos;
using Newtonsoft.Json.Serialization;
using Services;
using System.Security.Cryptography.X509Certificates;
using Models;
namespace CRUD_Unit16_Tests
  
{
    public class UnitTest1
    {

        #region testing for StockService.CreateBuyOrder
        [Fact]
        public async Task BuyOrderRequestIsNull_ShouldThrowArgumentNullException()
        {
            //arrange - create the object were testing
            var service = new StocksService();


            //Act + assert == needs to thow an exception
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateBuyOrder(null));
            
        }

        [Fact]
        public async Task BuyOrderQuantityIsZero_ReturnArgumentException()
        {

            var service = new StocksService();
            //arrange - create the object were testing
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockName = "Name",
                StockSymbol = "MSFT",
                Quantity = 0,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 22
            };


            //Act + assert == needs to thow an exception
            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateBuyOrder(request) );

        }

        [Fact]
        public async Task BuyOrderQuantityMore_ReturnArgumentException()
        {
            var service = new StocksService();

            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockName = "Name",
                StockSymbol = "AAP",
                Quantity = 100001,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 22
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateBuyOrder(request) );
        }

        [Fact]
        public async Task BuyOrderPriceZero_ThrowArgmumentException()
        {
            var service = new StocksService();
            BuyOrderRequest request = new BuyOrderRequest()
            {

                StockName = "Name",
                StockSymbol = "App",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 0, 
                Quantity = 2,
            };
        }

        [Fact]
        public async Task BuyOrderPriceHigher_ReturnArgumentException()
        {
            var service = new StocksService();
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockName = "Name",
                StockSymbol = "Sym",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 10001,
                Quantity = 2,
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateBuyOrder(request));
        }

        [Fact]
        public async Task SymbolNull_ReturnArgumentException()
        {
            var service = new StocksService();
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockName = "Name",
                StockSymbol = null,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 10,
                Quantity = 2,
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateBuyOrder(request));
        }

        [Fact]
        public async Task DateAndTimeOrderLower_ThrowArgumentException()
        {
            var service = new StocksService();

            BuyOrderRequest request = new BuyOrderRequest()
            {

                StockName = "mst",
                StockSymbol = "ss",
                DateAndTimeOfOrder = new DateTime(1999, 12, 31),
                Price = 12,
                Quantity = 3

            };

            await Assert.ThrowsAsync<ArgumentException> (() => service.CreateBuyOrder(request));
        }

        [Fact]
        public async Task IfAllCorrect_GenerateBuyOrderId()
        {
            var service = new StocksService();
            BuyOrderRequest request = new BuyOrderRequest()
            {

                StockName = "mst",
                StockSymbol = "ss",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 12,
                Quantity = 3

            };

            //act
          var response = await service.CreateBuyOrder(request);


            //assert 
           
            
                Assert.True(response.BuyOrderID != Guid.Empty);
            Assert.IsType<BuyOrderResponse>(response);

        }

        #endregion


        #region Testing for CreateSellOrder
        [Fact]
        public async void SellOrderRequestNull_ReturnArgumentNull()
        {
            var service = new StocksService();

           await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateSellOrder(null));
        }

        [Fact]
        public async void SellOrderQuantity0_ReturnArgumentException()
        {
            var service = new StocksService();
            SellOrderRequest request = new SellOrderRequest()
            {
                StockName = "name",
                StockSymbol = "ss",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 12,
                Quantity = 0
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateSellOrder(request));
        }
        [Fact]
        public async void SellOrderQuantity10001_ReturnArgumentException()
        {
            var service = new StocksService();
            SellOrderRequest request = new SellOrderRequest()
            {
                StockName = "name",
                StockSymbol = "ss",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 12,
                Quantity = 100001
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateSellOrder(request));
        }

        [Fact]
        public async void SellOrderPrice0_ReturnArgumentException()
        {
            var service = new StocksService();
            SellOrderRequest request = new SellOrderRequest()
            {
                StockName = "name",
                StockSymbol = "ss",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 0,
                Quantity = 100
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateSellOrder(request));
        }

        [Fact]
        public async void SellOrderPrice10001_ReturnArgumentException()
        {
            var service = new StocksService();
            SellOrderRequest request = new SellOrderRequest()
            {
                StockName = "name",
                StockSymbol = "ss",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 10001,
                Quantity = 100
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateSellOrder(request));
        }


        [Fact]
        public async void SellOrderStockSymbolNull_ReturnArgumentException()
        {
            var service = new StocksService();
            SellOrderRequest request = new SellOrderRequest()
            {
                StockName = "name",
                StockSymbol = null,
                DateAndTimeOfOrder = DateTime.Now,
                Price = 101,
                Quantity = 100
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateSellOrder(request));
        }

        [Fact]
        public async void SellOrderStockDateTime_ReturnArgumentException()
        {
            var service = new StocksService();
            SellOrderRequest request = new SellOrderRequest()
            {
                StockName = "name",
                StockSymbol = null,
                DateAndTimeOfOrder = new DateTime(1999, 12, 31),
                Price = 101,
                Quantity = 100
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateSellOrder(request));
        }

        [Fact]
        public async Task SellOrderRequestGood_ReturnSellerOrderId()
        {
            var service = new StocksService();
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockName = "name",
                StockSymbol = "ss",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 2,
                Quantity = 100
            };
            var response =await service.CreateSellOrder(sellOrderRequest);


            Assert.True(response.SellOrderID != Guid.Empty);
            Assert.IsType<SellOrderResponse>(response);


        }


        #endregion

        #region GetAllBuyOrders
        [Fact]
        public async Task GetAllBuyOrders_ReturnEmpty()
        {
            var service = new StocksService();
            //act
          var orders = await  service.GetBuyOrders();

            //assert 
            Assert.Empty(orders);

        }

        [Fact]

        public async Task CreateBuyOrders_RetrieveAll()
        {

            //arrage 
            var service = new StocksService();
          
            BuyOrderRequest request = new BuyOrderRequest()
            {
                StockName = "stock1",
                StockSymbol = "st1",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 2,
                Quantity = 120
            };
            await service.CreateBuyOrder(request);
            BuyOrderRequest request2 = new BuyOrderRequest()
            {
                StockName = "stock2",
                StockSymbol = "st2",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 4,
                Quantity = 20
            };
            await service.CreateBuyOrder(request2);

            //act
           var response = await service.GetBuyOrders();

            //assert
            Assert.Equal(request.StockName,response[0].StockName);
            Assert.Equal(request2.StockName, response[1].StockName);
            //assert
            Assert.Equal(request.StockSymbol, response[0].StockSymbol);
            Assert.Equal(request2.StockSymbol, response[1].StockSymbol);

            //assert
            Assert.Equal(request.Price, response[0].Price);
            Assert.Equal(request2.Price, response[1].Price);
            //assert
            Assert.Equal(request.Quantity, response[0].Quantity);
            Assert.Equal(request2.Quantity, response[1].Quantity);
            //assert
            Assert.Equal(request.DateAndTimeOfOrder, response[0].DateAndTimeOfOrder);
            Assert.Equal(request2.DateAndTimeOfOrder, response[1].DateAndTimeOfOrder);
        }


        [Fact]
        public async Task CreateSellOrder_ShouldReturnallsameListSellOrders()
        {
            var service = new StocksService();

            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockName = "Name",
                StockSymbol = "Sell",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 1,
                Quantity = 1,
            };

           await service.CreateSellOrder(sellOrderRequest);

            SellOrderRequest sellOrder2 = new SellOrderRequest()
            {
                StockName = "Name2",
                StockSymbol = "S2",
                DateAndTimeOfOrder = DateTime.Now,
                Price = 2,
                Quantity = 2,
            };
         await  service.CreateSellOrder(sellOrder2);

            var allSell =await service.GetSellOrders();

            Assert.Equal(sellOrderRequest.StockName, allSell[0].StockName);
            Assert.Equal(sellOrder2.StockName, allSell[1].StockName);
            Assert.Equal(sellOrderRequest.StockSymbol, allSell[0].StockSymbol);
            Assert.Equal(sellOrder2.StockSymbol, allSell[1].StockSymbol);


        }


        #endregion
    }
}
