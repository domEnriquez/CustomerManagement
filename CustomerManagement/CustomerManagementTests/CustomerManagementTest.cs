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
        private string simulatedCustDetailInput(Customer expectCust)
        {
            return string.Format(expectCust.CustomerID + "{0}" + expectCust.FirstName + "{0}" + expectCust.LastName +
                                    "{0}" + expectCust.Contact.Email + "{0}" + expectCust.Address.HomeAddress + "{0}" + expectCust.Address.City +
                                    "{0}" + expectCust.Address.State + "{0}" + expectCust.Address.ZipCode + "{0}" + expectCust.Contact.PhoneNumber +
                                    "{0}" + expectCust.CreditCard.Number + "{0}" + expectCust.CreditCard.Type + "{0}" +
                                    expectCust.CreditCard.ExpirationDate, Environment.NewLine);
        }

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

            Assert.AreEqual(expectCust, actualCust);

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void WhenSelectSearchByNameAndCustomerIsFound_ThenReturnCustomer()
        {
            repo.AddCustomer(new CustomerBuilder().withName("Dominic", "Enriquez").build());
            repo.AddCustomer(new CustomerBuilder().withId("2")
                                .withName("Roddick", "Quezon").build());
            repo.AddCustomer(new CustomerBuilder().withId("3").withName("Dominic", "Roque").build());

            menuItems = Program.BuildMenu(repo, ui);

            using (StringReader sr = new StringReader("Dom"))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.GetCustomerByName].ExecuteCommand();
            }

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void WhenSelectSearchByNameOptionAndNoCustomerIsFound_ThenReturnNoCustomersFound()
        {
            menuItems = Program.BuildMenu(repo, ui);

            using (StringReader sr = new StringReader("Dom"))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.GetCustomerByName].ExecuteCommand();
            }

            Approvals.Verify(fakeOutput);
        }
    }
}
