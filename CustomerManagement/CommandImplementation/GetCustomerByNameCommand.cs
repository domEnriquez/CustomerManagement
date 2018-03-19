using AbstractCommand;
using AbstractRepository;
using AbstractUserInterface;
using CustomerEntity;
using System.Collections.Generic;
using System.Linq;

namespace CommandImplementation
{
    public class GetCustomerByNameCommand : Command
    {
        private CustomerRepository custRepo;
        private UserInterface ui;

        public GetCustomerByNameCommand(CustomerRepository custRepo, UserInterface ui)
        {
            this.custRepo = custRepo;
            this.ui = ui;
        }

        public void Execute()
        {
            string name = ui.GetUserInput();
            IEnumerable<Customer> customers = custRepo.GetCustomersByName(name);

            if (customers.Count() == 0)
                ui.ShowNoCustomers();
            else
                ui.ShowCustomers(customers);
        }
    }
}