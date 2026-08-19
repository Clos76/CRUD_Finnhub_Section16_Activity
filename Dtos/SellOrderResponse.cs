using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string StockSymbol { get; set; } = null!;
        public string StockName { get; set; } = null!;
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }
    }
}
