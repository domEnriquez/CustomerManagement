using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class ConsoleUserInterface : UserInterface
    {
        public string GetUserInput()
        {
            string input = Console.ReadLine();
            return input;
        }

        public void ShowCustomer(Customer cust)
        {
            Console.WriteLine("---------------");
            Console.WriteLine("Customer ID: " + cust.CustomerID);
            Console.WriteLine("Name: " + cust.Name);
            Console.WriteLine("---------------");
        }

        public void ShowCustomers(IEnumerable<Customer> customers)
        {
            foreach (Customer cust in customers)
            {
                Console.WriteLine("---------------");
                Console.WriteLine("Customer ID: " + cust.CustomerID);
                Console.WriteLine("Name: " + cust.Name);
                Console.WriteLine("---------------");
            }
        }

        public void ShowNoCustomers()
        {
            Console.WriteLine("No Customers Found");
        }
    }
}
