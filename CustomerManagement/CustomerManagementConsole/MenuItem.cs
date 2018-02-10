using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementConsole
{
    public class MenuItem
    {
        public string Label { get; set; }
        private Command command;

        public MenuItem(Command com)
        {
            command = com;
        }

        public void ExecuteCommand()
        {
            command.Execute();
        }
    }
}
