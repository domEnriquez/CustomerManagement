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
            showTitle();

            List<MenuItem> menuItems = BuildMenu(new InMemoryCustomerRepository(), new ConsoleUserInterface());

            string input = string.Empty;

            do
            {
                showMenu(menuItems);

                input = askForCommand();

                menuItems[Convert.ToInt32(input) - 1].ExecuteCommand();

            } while (input != "5");

        }

        private static string askForCommand()
        {
            Console.Write("Enter a command:");
            string input = Console.ReadLine();

            return input;
        }

        private static void showMenu(List<MenuItem> menuItems)
        {
            Console.WriteLine();
            Console.WriteLine("COMMAND MENU");

            foreach (MenuItem menuItem in menuItems)
            {
                Console.WriteLine(menuItem.Label);
            }

            Console.WriteLine();
        }

        private static void showTitle()
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Welcome to the Customer Management Application");
            Console.WriteLine("-----------------------------------------------");
        }

        public static List<MenuItem> BuildMenu(CustomerRepository custRepo, UserInterface ui)
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            menuItems.Add(new MenuItem(new GetAllCustomersCommand(custRepo, ui), "1 - List all customers"));
            menuItems.Add(new MenuItem(new GetCustomerByIdCommand(custRepo, ui), "2 - Find customer"));
            menuItems.Add(new MenuItem(new GetCustomerByNameCommand(custRepo, ui), "3 - Find customer by name"));
            menuItems.Add(new MenuItem(new AddCustomerCommand(custRepo, ui), "4 - Add a customer"));
            menuItems.Add(new MenuItem(new ExitCommand(), "5 - Exit"));

            return menuItems;
        }

    }
}
