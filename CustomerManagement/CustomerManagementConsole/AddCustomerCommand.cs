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
            string id = getInput("Customer ID", new Required(new ReqLength(6, new NumberOnly(new DefaultValidator()))));
            string firstName = getInput("First Name", new Required(new DefaultValidator()));
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

        private string getInput(string fieldLabel, Validator v)
        {
            string fieldVal = ui.AskFor(fieldLabel);
            ValidationResult vr = v.Validate(fieldLabel, fieldVal);

            while (!vr.Result)
            {
                ui.ShowMessage(vr.Message);
                fieldVal = ui.AskFor(fieldLabel);
                vr = v.Validate(fieldLabel, fieldVal);
            }

            return fieldVal;
        }
    }
}
