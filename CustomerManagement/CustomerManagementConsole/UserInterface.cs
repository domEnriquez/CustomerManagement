using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public interface UserInterface
    {
        void ShowNoCustomers();

        void ShowCustomers(IEnumerable<Customer> customers);

        void ShowCustomer(Customer customer);

        void ShowCustomerIsSaved();

        string GetUserInput();

        string AskFor(string fieldLabel);
    }
}
