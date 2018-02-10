using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }

        public Customer(int id, string name)
        {
            CustomerID = id;
            Name = name;
        }
    }
}
