using AbstractCommand;
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

        public MenuItem(Command com, string label)
        {
            command = com;
            Label = label;
        }

        public void ExecuteCommand()
        {
            command.Execute();
        }
    }
}
