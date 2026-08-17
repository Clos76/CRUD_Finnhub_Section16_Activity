using CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace Dtos
{
    public class BuyOrderRequest
    {
        [Required]
        public string StockSymbol { get; set; } = null!;
        [Required]
        public string StockName { get; set; } = null!;

        [MinimumDateAttribute(2000, ErrorMessage ="Minimum year allowed is Jan 1, 2000")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public int Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }

    }
}

