using CustomerEntity;
using CustomerManagementConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementTests.TestDataBuilders
{
    public class CreditCardBuilder
    {
        private string cardNumber = "1234";
        private string cardType = "VISA";
        private string cardExpirationDate = "01-01-2022";

        public CreditCard build()
        {
            return new CreditCard(cardNumber, cardType, cardExpirationDate);
        }
    }
}
