using AbstractCommand;
using AbstractRepository;
using AbstractUserInterface;
using CustomerEntity;

namespace CommandImplementation
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
            string id = ui.AskFor("Enter Customer ID");
            Customer cust = custRepo.GetCustomerById(id);

            if (cust == null)
                ui.ShowNoCustomers();
            else
                ui.ShowCustomer(cust);
        }
    }
}
