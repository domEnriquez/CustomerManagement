using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class GetCustomerByIdCommand : Command
    {
        CustomerRepository custRepo;
        UserInterface ui;

        public GetCustomerByIdCommand(CustomerRepository repo, UserInterface ui)
        {
            custRepo = repo;
            this.ui = ui;
        }

        public void Execute()
        {
            string id = ui.GetUserInput();
            Customer cust = custRepo.GetCustomerById(id);

            if (cust == null)
                ui.ShowNoCustomers();
            else
                ui.ShowCustomer(custRepo.GetCustomerById(id));
        }
    }
}
