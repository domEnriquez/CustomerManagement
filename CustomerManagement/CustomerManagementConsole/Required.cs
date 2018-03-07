using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class Required : ValidatorDecorator
    {
        public Required(Validator v) : base(v)
        {
        }

        public override ValidationResult Validate(string fieldLabel, string fieldValue)
        {
            ValidationResult result = new ValidationResult();

            if (fieldValue == string.Empty)
            {
                result.Result = false;
                result.Message = fieldLabel + " is required.";
                return result;
            }

            return base.Validate(fieldLabel, fieldValue);
        }
    }
}
