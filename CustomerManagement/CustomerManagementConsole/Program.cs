using AbstractRepository;
using AbstractUserInterface;
using CommandImplementation;
using RepositoryImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<MenuItem> menuItems = BuildMenu(new InMemoryCustomerRepository(), new ConsoleUserInterface());
        }

        public static List<MenuItem> BuildMenu(CustomerRepository custRepo, UserInterface ui)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            menuItems.Add(new MenuItem(new GetAllCustomersCommand(custRepo, ui)));
            menuItems.Add(new MenuItem(new GetCustomerByIdCommand(custRepo, ui)));
            menuItems.Add(new MenuItem(new GetCustomerByNameCommand(custRepo, ui)));
            menuItems.Add(new MenuItem(new AddCustomerCommand(custRepo, ui)));

            return menuItems;
        }

    }
}
