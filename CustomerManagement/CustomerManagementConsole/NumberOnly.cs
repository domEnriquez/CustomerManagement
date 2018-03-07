using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class NumberOnly : ValidatorDecorator
    {
        public NumberOnly(Validator v) : base(v)
        {
        }

        public override ValidationResult Validate(string fieldLabel, string fieldValue)
        {
            ValidationResult result = new ValidationResult();
            double num;

            if (!double.TryParse(fieldValue, out num))
            {
                result.Result = false;
                result.Message = fieldLabel + " is not a number.";
                return result;
            }

            return base.Validate(fieldLabel, fieldValue);
        }
    }
}
