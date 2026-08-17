
using System.ComponentModel.DataAnnotations;


namespace CustomValidators
{
    public class SellOrderDateValidator :ValidationAttribute
    {
        public DateTime MinDate { get; set; } = new DateTime(2000, 1, 1);
        public SellOrderDateValidator()
        {
            
        }
        public SellOrderDateValidator(int year)
        {
            MinDate = new DateTime(year, 1, 1);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            

            if(value != null)
            {
                DateTime date = (DateTime)value;

                if(date < MinDate)
                {
                    return new ValidationResult($"Date can not be earlier than {MinDate: MMMM dd, yyyy}");
                }
               
            }
            return ValidationResult.Success;
        }
    }
}
