using CustomerManagementConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementTests.TestDataBuilders
{
    public class AddressBuilder
    {
        private string homeAddress = "Home";
        private string city = "City";
        private string state = "State";
        private string zipCode = "1234";

        public Address build()
        {
            return new Address(homeAddress, city, state, zipCode);
        }
    }
}
