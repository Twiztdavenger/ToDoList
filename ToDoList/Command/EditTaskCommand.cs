using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.ViewModel;

using System.Data.SQLite;
using ToDoList.Database;

namespace ToDoList.Command
{
    public class EditTaskCommand : ICommand 
    {
        ManageDatabase md = new ManageDatabase();

        public event EventHandler CanExecuteChanged;

        //This makes sure we can always execute the action
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is TaskViewModel task)
            {
                var addTask = new TaskDialog(task);
            }
        }
    }
}
