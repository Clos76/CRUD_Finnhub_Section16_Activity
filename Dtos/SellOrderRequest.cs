using CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class SellOrderRequest
    {

        [Required(ErrorMessage ="The Stock Symbol is mandatory")]
        public string StockSymbol { get; set; } = null!;
        [Required(ErrorMessage ="The Stock Name is mandatory")]
        public string StockName { get; set; } = null!;
        [SellOrderDateValidator(ErrorMessage ="Date can not be longer than Jan 01, 2000")]
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000, ErrorMessage = "Range is from 1-100000")]
        public uint Quantity { get; set; }
        [Range(1, 10000, ErrorMessage = "Range is from 1-10000")]
        public double Price { get; set; }
    }
}
