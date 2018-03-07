using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public abstract class ValidatorDecorator : Validator
    {
        protected Validator Validator;

        public ValidatorDecorator(Validator v)
        {
            Validator = v;
        }

        public override ValidationResult Validate(string fieldLabel, string fieldValue)
        {
            return Validator.Validate(fieldLabel, fieldValue);
        }
    }
}
