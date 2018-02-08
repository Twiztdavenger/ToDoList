using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoList.Database;
using ToDoList.ViewModel;

namespace ToDoList.Command 
{
    public class DeleteTaskCommand : ICommand
    {
        ManageDatabase md = new ManageDatabase();

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)


        {

            if (parameter is TaskViewModel task)
            {
                // TODO: Show name of task you're deleting
                // TODO: Allow multiple selection
                

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;

                MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure you want to delete?", "Delete Task", btnMessageBox);

                if(rsltMessageBox == MessageBoxResult.Yes)
                {
                    md.removeTaskRecord(task);

                }
                    


            }
        }
    }
}
