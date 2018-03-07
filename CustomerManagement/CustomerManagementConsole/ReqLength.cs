using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class ReqLength : ValidatorDecorator
    {
        private int reqLength;

        public ReqLength(int reqLength, Validator v) : base(v)
        {
            this.reqLength = reqLength;
        }

        public override ValidationResult Validate(string fieldLabel, string fieldValue)
        {
            ValidationResult vr = new ValidationResult();

            if(fieldValue.Length != reqLength)
            {
                vr.Result = false;
                vr.Message = fieldLabel + " must be exactly " + reqLength + " characters.";

                return vr;
            }

            return base.Validate(fieldLabel, fieldValue);
        }
    }
}
