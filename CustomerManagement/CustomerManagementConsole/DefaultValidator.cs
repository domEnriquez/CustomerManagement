using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class DefaultValidator : Validator
    {
        public override ValidationResult Validate(string fieldLabel, string fieldValue)
        {
            ValidationResult vr = new ValidationResult();
            vr.Result = true;

            return vr;
        }
    }
}
