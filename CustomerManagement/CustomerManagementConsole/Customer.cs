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

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public Customer(string id, string fn, string ln, Address addr, Contact contact, CreditCard cc)
        {
            CustomerID = id;
            FirstName = fn;
            LastName = ln;
            Address = addr;
            Contact = contact;
            CreditCard = cc;
        }

        public override bool Equals(object customer)
        {
            if (!(customer is Customer))
                return false;

            Customer otherCust = (Customer)customer;

            if (CustomerID != otherCust.CustomerID) return false;
            if (FirstName != otherCust.FirstName) return false;
            if (LastName != otherCust.LastName) return false;
            if (!Address.Equals(otherCust.Address)) return false;
            if (!Contact.Equals(otherCust.Contact)) return false;
            if (!CreditCard.Equals(otherCust.CreditCard)) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
