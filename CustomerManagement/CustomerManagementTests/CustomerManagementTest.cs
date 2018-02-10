using ApprovalTests;
using ApprovalTests.Reporters;
using CustomerManagementConsole;
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
            repo.AddCustomer(new Customer(1, "Dom"));
            repo.AddCustomer(new Customer(2, "Roddick"));

            menuItems = Program.BuildMenu(repo, ui);

            menuItems[(int)menuEnum.GetAllCustomers].ExecuteCommand();

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void GivenACustomerIsAdded_WhenSelectSearchByCustomerIdOptionAndCustomerIsFound_ThenReturnCustomer()
        {
            repo.AddCustomer(new Customer(1, "Dom"));
            repo.AddCustomer(new Customer(2, "Roddick"));
            
            using (StringReader sr = new StringReader("1"))
            {
                Console.SetIn(sr);

                menuItems = Program.BuildMenu(repo, ui);

                menuItems[(int)menuEnum.GetCustomerById].ExecuteCommand();
            }

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void WhenSelectSearchByCustomerIdOptionAndNoCustomerIsFound_ThenReturnNoCustomersFound()
        {
            using (StringReader sr = new StringReader("2"))
            {
                Console.SetIn(sr);

                menuItems = Program.BuildMenu(repo, ui);

                menuItems[(int)menuEnum.GetCustomerById].ExecuteCommand();
            }

            Approvals.Verify(fakeOutput);
        }
    }
}
