using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SellOrder
    {
        public Guid SellOrderID { get; set; }
        [Required]
        public string StockSymbol { get; set; } = null!;
        [Required]
        public string StockName { get; set; } = null!;
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000)]
        public uint Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }
    }
}
