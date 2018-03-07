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

        private string simuCustDetailInputWithCorrectedId(Customer wrongIdCust, int wrongInputCount, string correctId)
        {
            string wrongIdInputs = string.Empty;

            for (int i = 1; i <= wrongInputCount; i++)
                if (i == wrongInputCount)
                    wrongIdInputs += wrongIdCust.CustomerID;
                else
                    wrongIdInputs += wrongIdCust.CustomerID + "{0}";
            

            return string.Format(wrongIdInputs + "{0}" + correctId + "{0}" + wrongIdCust.FirstName + "{0}" + wrongIdCust.LastName +
                                    "{0}" + wrongIdCust.Contact.Email + "{0}" + wrongIdCust.Address.HomeAddress + "{0}" + wrongIdCust.Address.City +
                                    "{0}" + wrongIdCust.Address.State + "{0}" + wrongIdCust.Address.ZipCode + "{0}" + wrongIdCust.Contact.PhoneNumber +
                                    "{0}" + wrongIdCust.CreditCard.Number + "{0}" + wrongIdCust.CreditCard.Type + "{0}" +
                                    wrongIdCust.CreditCard.ExpirationDate, Environment.NewLine);
        }

        private StringBuilder fakeOutput;
        private CustomerRepository repo;
        private UserInterface ui;
        private List<MenuItem> menuItems;

        [SetUp]
        public void SetUp()
        {

            repo = new InMemoryCustomerRepository();
            ui = new ConsoleUserInterface();
            menuItems = Program.BuildMenu(repo, ui);
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
            menuItems[(int)menuEnum.GetAllCustomers].ExecuteCommand();

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void GivenACustomerIsAdded_WhenSelectGetAllCustomersOption_ThenReturnAllCustomers()
        {
            repo.AddCustomer(new CustomerBuilder().build());
            repo.AddCustomer(new CustomerBuilder().withId("000002")
                                .withName("Roddick", "Quezon").build());

            menuItems[(int)menuEnum.GetAllCustomers].ExecuteCommand();

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void GivenACustomerIsAdded_WhenSelectSearchByCustomerIdOptionAndCustomerIsFound_ThenReturnCustomer()
        {
            repo.AddCustomer(new CustomerBuilder().build());
            repo.AddCustomer(new CustomerBuilder().withId("000002")
                                .withName("Roddick", "Quezon").build());

            using (StringReader sr = new StringReader("000001"))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.GetCustomerById].ExecuteCommand();
            }

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void WhenSelectSearchByCustomerIdOptionAndNoCustomerIsFound_ThenReturnNoCustomersFound()
        {
            using (StringReader sr = new StringReader("000001"))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.GetCustomerById].ExecuteCommand();
            }

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void WhenSelectAddCustomerAndNoInvalidInputs_ThenStoreCustomer()
        {
            Customer expectCust = new CustomerBuilder().withId("000008").build();

            using(StringReader sr = new StringReader(simulatedCustDetailInput(expectCust)))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.AddCustomer].ExecuteCommand();
            }

            Customer actualCust = repo.GetCustomerById("000008");

            Assert.AreEqual(expectCust, actualCust);

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void GivenEmptyCustomerId_WhenAddCustomer_ThenAskForCustomerIdAgain()
        {
            Customer expectCust = new CustomerBuilder().withId("").build();
            assertAskedForCustIdAgain(expectCust);
        }

        [Test]
        public void GivenCustIdHasInvalidLength_WhenAddCustomer_ThenAskForCustomerIdAgain()
        {
            Customer expectCust = new CustomerBuilder().withId("001").build();
            assertAskedForCustIdAgain(expectCust);
        }

        [Test]
        public void GivenANonNumberCustId_WhenAddCustomer_ThenAskForCustomerIdAgain()
        {
            Customer expectCust = new CustomerBuilder().withId("abcdef").build();
            assertAskedForCustIdAgain(expectCust);
        }

        private void assertAskedForCustIdAgain(Customer expectCust)
        {
            string validCustId = "000008";

            using (StringReader sr = new StringReader(simuCustDetailInputWithCorrectedId(expectCust, 4, validCustId)))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.AddCustomer].ExecuteCommand();
            }

            Customer actualCust = repo.GetCustomerById(validCustId);

            expectCust.CustomerID = validCustId;
            Assert.AreEqual(expectCust, actualCust);

            Approvals.Verify(fakeOutput);
        }

        [Test]
        public void WhenSelectSearchByNameAndCustomerIsFound_ThenReturnCustomer()
        {
            repo.AddCustomer(new CustomerBuilder().withName("Dominic", "Enriquez").build());
            repo.AddCustomer(new CustomerBuilder().withId("000002")
                                .withName("Roddick", "Quezon").build());
            repo.AddCustomer(new CustomerBuilder().withId("000003").withName("Dominic", "Roque").build());

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
            using (StringReader sr = new StringReader("Dom"))
            {
                Console.SetIn(sr);
                menuItems[(int)menuEnum.GetCustomerByName].ExecuteCommand();
            }

            Approvals.Verify(fakeOutput);
        }
    }
}
