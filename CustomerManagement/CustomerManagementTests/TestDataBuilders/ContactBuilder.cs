using CustomerManagementConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementTests.TestDataBuilders
{
    public class ContactBuilder
    {
        private string email = "d@y.com";
        private string phoneNumber = "512-1235";

        public Contact build()
        {
            return new Contact(email, phoneNumber);
        }
    }
}
