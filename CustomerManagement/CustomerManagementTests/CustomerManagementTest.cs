using AbstractRepository;
using AbstractUserInterface;
using ApprovalTests;
using ApprovalTests.Reporters;
using CustomerEntity;
using CustomerManagementConsole;
using CustomerManagementTests.TestDataBuilders;
using NUnit.Framework;
using RepositoryImplementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomerManagementTests
{
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

        [TestFixture]
        [UseReporter(typeof(DiffReporter))]
        public class AcceptanceTests : CustomerManagementTest
        {
            [Test]
            public void WhenApplicationStarts_ThenBuildAndShowMenu()
            {
                using (StringReader sr = new StringReader("5"))
                {
                    Console.SetIn(sr);
                    Program.Main(new string[] { });
                }

                Approvals.Verify(fakeOutput);
            }

            [Test]
            public void GivenNoCustomerAdded_WhenSelectGetAllCustomersOption_ThenReturnNoCustomersFound()
            {
                menuItems[(int)MenuEnum.GetAllCustomers].ExecuteCommand();

                Approvals.Verify(fakeOutput);
            }

            [Test]
            public void GivenACustomerIsAdded_WhenSelectGetAllCustomersOption_ThenReturnAllCustomers()
            {
                repo.AddCustomer(new CustomerBuilder().build());
                repo.AddCustomer(new CustomerBuilder().withId("000002")
                                    .withName("Roddick", "Quezon").build());

                menuItems[(int)MenuEnum.GetAllCustomers].ExecuteCommand();

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
                    menuItems[(int)MenuEnum.GetCustomerById].ExecuteCommand();
                }

                Approvals.Verify(fakeOutput);
            }

            [Test]
            public void WhenSelectSearchByCustomerIdOptionAndNoCustomerIsFound_ThenReturnNoCustomersFound()
            {
                using (StringReader sr = new StringReader("000001"))
                {
                    Console.SetIn(sr);
                    menuItems[(int)MenuEnum.GetCustomerById].ExecuteCommand();
                }

                Approvals.Verify(fakeOutput);
            }

            [Test]
            public void WhenSelectAddCustomerAndNoInvalidInputs_ThenStoreCustomer()
            {
                Customer expectCust = new CustomerBuilder().withId("000008").build();

                using (StringReader sr = new StringReader(simulatedCustDetailInput(expectCust)))
                {
                    Console.SetIn(sr);
                    menuItems[(int)MenuEnum.AddCustomer].ExecuteCommand();
                }

                Customer actualCust = repo.GetCustomerById("000008");

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
                    menuItems[(int)MenuEnum.GetCustomerByName].ExecuteCommand();
                }

                Approvals.Verify(fakeOutput);
            }

            [Test]
            public void WhenSelectSearchByNameOptionAndNoCustomerIsFound_ThenReturnNoCustomersFound()
            {
                using (StringReader sr = new StringReader("Dom"))
                {
                    Console.SetIn(sr);
                    menuItems[(int)MenuEnum.GetCustomerByName].ExecuteCommand();
                }

                Approvals.Verify(fakeOutput);
            }
        }


        [TestFixture]
        [UseReporter(typeof(DiffReporter))]
        public class AddCustomerValidationTests : CustomerManagementTest
        {
            private void assertAskedForFirstNameAgain(Customer expectCust)
            {
                string validCustName = "Dominic";

                using (StringReader sr = new StringReader(simulateAddCustWithCorrectedInput(expectCust, 1, 4, validCustName)))
                {
                    Console.SetIn(sr);
                    menuItems[(int)MenuEnum.AddCustomer].ExecuteCommand();
                }

                Customer actualCust = repo.GetCustomerById("000001");

                expectCust.FirstName = validCustName;
                Assert.AreEqual(expectCust, actualCust);

                Approvals.Verify(fakeOutput);
            }

            private void assertAskedForCustIdAgain(Customer expectCust)
            {
                string validCustId = "000008";

                using (StringReader sr = new StringReader(simulateAddCustWithCorrectedInput(expectCust, 0, 4, validCustId)))
                {
                    Console.SetIn(sr);
                    menuItems[(int)MenuEnum.AddCustomer].ExecuteCommand();
                }

                Customer actualCust = repo.GetCustomerById(validCustId);

                expectCust.CustomerID = validCustId;
                Assert.AreEqual(expectCust, actualCust);

                Approvals.Verify(fakeOutput);
            }

            private string simulateAddCustWithCorrectedInput(Customer cust, int wrongInputPos, int wrongInputCount, string correctInput)
            {
                string simulatedInput = simulatedCustDetailInput(cust);
                string[] splitInput = simulatedInput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string wrongInputs = replicateInput(splitInput[wrongInputPos], wrongInputCount);

                splitInput[wrongInputPos] = wrongInputs + correctInput;

                return string.Join(Environment.NewLine, splitInput);
            }

            private string replicateInput(string wrongInput, int wrongInputCount)
            {
                string wrongInputs = string.Empty;

                for (int i = 1; i <= wrongInputCount; i++)
                    wrongInputs += wrongInput + Environment.NewLine;

                return wrongInputs;
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

            [Test]
            public void GivenEmptyFirstName_WhenAddCustomer_ThenAddForCustomerNameAgain()
            {
                Customer expectCust = new CustomerBuilder().withName("", "Enriquez").build();
                assertAskedForFirstNameAgain(expectCust);
            }

        }
    }
}
