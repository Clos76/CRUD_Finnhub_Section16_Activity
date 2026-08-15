using System.ComponentModel.DataAnnotations;

namespace Dtos
{
    public class BuyOrderRequest
    {
        [Required]
        public string StockSymbol { get; set; } = null!;
        [Required]
        public string StockName { get; set; } = null!;
        [Datetime, "20000-01-01")]
        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 100000)]
        public int Quantity { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }

    }
}

public class MinimumDateAttribute: ValidationAttribute
{
    private readonly DateTime _years;
    public MinimumDateAttribute(DateTime years)
    {
        _years = years;
    }
    //takes min date as constructor
    //overids isValid(obj value , validation context)
    //compares the incoming datetime to the min. 

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is)
        {
            if()
        }

        return 
    }
}