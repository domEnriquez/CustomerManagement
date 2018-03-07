using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public interface UserInterface
    {
        string GetUserInput();

        string AskFor(string fieldLabel);

        void ShowMessage(string message);

        void ShowNoCustomers();

        void ShowCustomers(IEnumerable<Customer> customers);

        void ShowCustomer(Customer customer);

        void ShowCustomerIsSaved();

        void ShowRequired(string fieldLabel);

        void ShowReqLength(string fieldLabel, int reqLength);
    }
}
