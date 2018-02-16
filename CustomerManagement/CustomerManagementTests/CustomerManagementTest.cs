using ApprovalTests;
using ApprovalTests.Reporters;
using CustomerManagementConsole;
using CustomerManagementTests.TestDataBuilders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomerManagementTests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class CustomerManagementTest
    {
        private StringBuilder fakeOutput;
        private CustomerRepository repo;
        private UserInterface ui;
        private List<MenuItem> menuItems;

        [SetUp]
        public void SetUp()
        {
            menuItems = new List<MenuItem>();
            repo = new InMemoryCustomerRepository();
            ui = new ConsoleUserInterface();
            fakeOutput = new StringBuilder();
            Console.SetOut(new StringWriter(fakeOutput));
        }

        [TearDown]
        public void TearDown()
        {
            repo.DeleteAllCustomers();
        }

        [Test]
        public void GivenNoCustomerAdded_WhenSelectGetAllCustomersOption_ThenReturnNoCustomersFound()
        {
            menuItems = Program.BuildMenu(repo, ui);
            menuItems[(int)menuEnum.GetAllCustomers].ExecuteCommand();

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void GivenACustomerIsAdded_WhenSelectGetAllCustomersOption_ThenReturnAllCustomers()
        {
            repo.AddCustomer(new CustomerBuilder().build());
            repo.AddCustomer(new CustomerBuilder().withId("2")
                                .withName("Roddick", "Quezon").build());

            menuItems = Program.BuildMenu(repo, ui);

            menuItems[(int)menuEnum.GetAllCustomers].ExecuteCommand();

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void GivenACustomerIsAdded_WhenSelectSearchByCustomerIdOptionAndCustomerIsFound_ThenReturnCustomer()
        {
            repo.AddCustomer(new CustomerBuilder().build());
            repo.AddCustomer(new CustomerBuilder().withId("2")
                                .withName("Roddick", "Quezon").build());

            menuItems = Program.BuildMenu(repo, ui);

            using (StringReader sr = new StringReader("1"))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.GetCustomerById].ExecuteCommand();
            }

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void WhenSelectSearchByCustomerIdOptionAndNoCustomerIsFound_ThenReturnNoCustomersFound()
        {
            menuItems = Program.BuildMenu(repo, ui);

            using (StringReader sr = new StringReader("1"))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.GetCustomerById].ExecuteCommand();
            }

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void WhenSelectAddCustomerAndNoInvalidInputs_ThenStoreCustomer()
        {
            Customer expectCust = new CustomerBuilder().withId("8").build();

            menuItems = Program.BuildMenu(repo, ui);

            using(StringReader sr = new StringReader(simulatedCustDetailInput(expectCust)))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.AddCustomer].ExecuteCommand();
            }

            Customer actualCust = repo.GetCustomerById("8");

            Assert.AreEqual(expectCust.CustomerID, actualCust.CustomerID);
            Assert.AreEqual(expectCust.FirstName, actualCust.FirstName);
            Assert.AreEqual(expectCust.LastName, actualCust.LastName);
            Assert.AreEqual(expectCust.Contact.Email, actualCust.Contact.Email);
            Assert.AreEqual(expectCust.Address.HomeAddress, actualCust.Address.HomeAddress);
            Assert.AreEqual(expectCust.Address.City, actualCust.Address.City);
            Assert.AreEqual(expectCust.Address.State, actualCust.Address.State);
            Assert.AreEqual(expectCust.Address.ZipCode, actualCust.Address.ZipCode);
            Assert.AreEqual(expectCust.Contact.PhoneNumber, actualCust.Contact.PhoneNumber);
            Assert.AreEqual(expectCust.CreditCard.Number, actualCust.CreditCard.Number);
            Assert.AreEqual(expectCust.CreditCard.Type, actualCust.CreditCard.Type);
            Assert.AreEqual(expectCust.CreditCard.ExpirationDate, actualCust.CreditCard.ExpirationDate);

            Approvals.Verify(fakeOutput);
        }

        private string simulatedCustDetailInput(Customer expectCust)
        {
            return string.Format(expectCust.CustomerID + "{0}" + expectCust.FirstName + "{0}" + expectCust.LastName +
                                    "{0}" + expectCust.Contact.Email + "{0}" + expectCust.Address.HomeAddress + "{0}" + expectCust.Address.City +
                                    "{0}" + expectCust.Address.State + "{0}" + expectCust.Address.ZipCode + "{0}" + expectCust.Contact.PhoneNumber +
                                    "{0}" + expectCust.CreditCard.Number + "{0}" + expectCust.CreditCard.Type + "{0}" + 
                                    expectCust.CreditCard.ExpirationDate, Environment.NewLine);
        }
    }
}
