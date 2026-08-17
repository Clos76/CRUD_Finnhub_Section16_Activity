using System.ComponentModel.DataAnnotations;

namespace CustomValidators
{
    public class MinimumDateAttribute : ValidationAttribute
    {
        //value is the one comparing-, 
        //ValidationContext is the one being validated-reference as validation context.

        //to pass min and max, you need to create a parameterless and a parameter 
        //also create a property to pass the min year to the parameterized ctor
        public DateTime MinDate { get; set; } = new DateTime(2000, 1, 1);
        public MinimumDateAttribute()
        {
            
        }
        public MinimumDateAttribute(int year)
        {
            MinDate = new DateTime(year, 1, 1);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value; //convert value into a DateTime - Store it as a var date
                //then check year
                if (date < MinDate)
                {
                    return new ValidationResult($"Date can not be earlier than {MinDate:MMMM dd, yyyy}");
                }
            }
           return ValidationResult.Success;
        }
    }
}
