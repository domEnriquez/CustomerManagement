using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class AddCustomerCommand : Command
    {
        CustomerRepository custRepo;
        UserInterface ui;

        public AddCustomerCommand(CustomerRepository repo, UserInterface ui)
        {
            custRepo = repo;
            this.ui = ui;
        }

        public void Execute()
        {
            string id = ui.AskFor("Customer ID");

            if (id == string.Empty)
            {
                Console.WriteLine("Customer ID is required.");
                id = ui.AskFor("Customer ID");
            }

            if(id.Length != 6)
            {
                Console.WriteLine("Customer ID must be exactly 6 characters.");
                id = ui.AskFor("Customer ID");
            }

            string firstName = ui.AskFor("First Name");
            string lastName = ui.AskFor("Last Name");
            string email = ui.AskFor("Email");
            string homeAddress = ui.AskFor("Home Address");
            string city = ui.AskFor("City");
            string state = ui.AskFor("State");
            string zipCode = ui.AskFor("Zip Code");
            string phoneNumber = ui.AskFor("Phone Number");
            string ccNumber = ui.AskFor("Credit Card Number");
            string ccType = ui.AskFor("Credit Card Type");
            string ccExpiry = ui.AskFor("Credit Card Expiration Date");

            Address address = new Address(homeAddress, city, state, zipCode);
            CreditCard cc = new CreditCard(ccNumber, ccType, ccExpiry);
            Contact contact = new Contact(email, phoneNumber);
            Customer cust = new Customer(id, firstName, lastName, address, contact, cc);

            custRepo.AddCustomer(cust);

            ui.ShowCustomerIsSaved();
        }
    }
}
