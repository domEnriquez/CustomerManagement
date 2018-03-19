using CustomerEntity;
using System.Collections.Generic;

namespace AbstractUserInterface
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
