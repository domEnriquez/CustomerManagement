using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public abstract class Validator
    {
        public abstract ValidationResult Validate(string fieldLabel, string fieldValue);
    }
}
