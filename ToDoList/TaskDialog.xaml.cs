using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoList.ViewModel;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for TaskDialogxaml.xaml
    /// </summary>
    public partial class TaskDialog : Window
    {
        int ID;
        string name;
        int priority;
        bool complete;

        TaskListViewModel tasks;

        string TaskName = "";

        public TaskDialog(TaskListViewModel tasks)
        {

            this.tasks = tasks;
            
            InitializeComponent();

            SubmitButton.Content = "Add Task";

            this.DataContext = tasks;
        }

        public TaskDialog(TaskViewModel tsk, TaskListViewModel tasks)
        {
            InitializeComponent();

            SubmitButton.Content = "Apply";

            tasks.ID = tsk.ID;
            tasks.TaskName = tsk.Name;
            tasks.Priority = tsk.Priority;
            tasks.Complete = tsk.Complete;
            this.tasks = tasks;

            RadioButton rb = new RadioButton();

             switch(tasks.Priority)
            {
                case 1:
                    noPr.IsChecked = true;
                    break;
                case 2:
                    somePr.IsChecked = true;
                    break;
                case 3:
                    veryPr.IsChecked = true;
                    break;
            }

            completeCB.IsChecked = tasks.Complete;

            this.DataContext = tasks; 
        }

        private void radioButtons_CheckedChanged(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            string rbName = radioButton.Name;

            if (rbName == "noPr")
            {
                priority = 1;
            }
            else if (rbName == "somePr")
            {
                priority = 2;
            }
            else if (rbName == "veryPr")
            {
                priority = 3;
            }

            tasks.Priority = priority;
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
