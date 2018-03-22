using AbstractUserInterface;
using CustomerEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class ConsoleUserInterface : UserInterface
    {
        public string AskFor(string fieldLabel)
        {
            Console.Write(fieldLabel + ": ");
            return GetUserInput();
        }

        public string GetUserInput()
        {
            string input = Console.ReadLine();
            return input;
        }

        public void ShowCustomer(Customer cust)
        {
            Console.WriteLine("---------------");
            Console.WriteLine("Customer ID: " + cust.CustomerID);
            Console.WriteLine("Name: " + cust.FullName);
            Console.WriteLine("Email: " + cust.Contact.Email);
            Console.WriteLine("Phone: " + cust.Contact.PhoneNumber);
            Console.WriteLine("Home Address: " + cust.Address.HomeAddress);
            Console.WriteLine("City: " + cust.Address.City);
            Console.WriteLine("State: " + cust.Address.State);
            Console.WriteLine("Zip Code: " + cust.Address.ZipCode);
            Console.WriteLine("Credit Card No: " + cust.CreditCard.Number);
            Console.WriteLine("Credit Card Type: " + cust.CreditCard.Type);
            Console.WriteLine("Credit Card Expiration Date: " + cust.CreditCard.ExpirationDate);

            Console.WriteLine("---------------");
        }

        public void ShowCustomerIsSaved()
        {
            Console.WriteLine();
            Console.WriteLine("Customer data successfully saved.");
        }

        public void ShowCustomers(IEnumerable<Customer> customers)
        {
            foreach (Customer cust in customers)
                ShowCustomer(cust);
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowNoCustomers()
        {
            Console.WriteLine("No Customers Found");
        }

        public void ShowReqLength(string fieldLabel, int reqLength)
        {
            Console.WriteLine(fieldLabel + " must be exactly " + reqLength + " characters.");
        }

        public void ShowRequired(string fieldLabel)
        {
            Console.WriteLine(fieldLabel + " is required.");
        }
    }
}
