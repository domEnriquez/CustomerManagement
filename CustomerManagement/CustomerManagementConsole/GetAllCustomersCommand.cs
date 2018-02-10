using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class GetAllCustomersCommand : Command
    {
        CustomerRepository custRepo;
        UserInterface ui;

        public GetAllCustomersCommand(CustomerRepository repo, UserInterface ui)
        {
            custRepo = repo;
            this.ui = ui;
        }

        public void Execute()
        {
            IEnumerable<Customer> customers = custRepo.GetAllCustomers();

            if (customers.Count() == 0)
                ui.ShowNoCustomers();
            else
                ui.ShowCustomers(customers);
        }
    }
}
