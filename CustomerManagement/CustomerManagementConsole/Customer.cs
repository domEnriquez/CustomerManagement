using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Address Address { get; set; }
        public Contact Contact { get; set; }
        public CreditCard CreditCard { get; set; }

        public Customer(string id, string fn, string ln, Address addr, Contact contact, CreditCard cc)
        {
            CustomerID = id;
            FirstName = fn;
            LastName = ln;
            Address = addr;
            Contact = contact;
            CreditCard = cc;
        }
    }
}
