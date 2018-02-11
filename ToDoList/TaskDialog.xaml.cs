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
    /// This is the window to edit or add tasks to our list
    /// 
    /// I was able to put the add/edit logic into one window
    /// This makes everything much less redundant 
    /// 
    /// </summary>
    public partial class TaskDialog : Window
    {
        // These are values we need to store for later
        int ID;
        string name;
        int priority;
        bool complete;

        TaskListViewModel tasks;

        string TaskName = "";
        
        // If we are only adding a task, all we need is the list to pass through
        public TaskDialog(TaskListViewModel tasks)
        {
            tasks.resetViewModel();

            this.tasks = tasks;
            
            InitializeComponent();

            SubmitButton.Content = "Add Task";

            this.DataContext = tasks;
        }

        // If we are editing a task, we need to pass through the task itself and the list
        public TaskDialog(TaskViewModel tsk, TaskListViewModel tasks)
        {
            InitializeComponent();

            SubmitButton.Content = "Apply";

            // This populates our ListView with the task data we are editing
            tasks.ID = tsk.ID;
            tasks.TaskName = tsk.Name;
            tasks.Priority = tsk.Priority;
            tasks.Complete = tsk.Complete;
            this.tasks = tasks;


            // This checks the appropriate radio button according to the priority of the
            // task we are editing
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

            // Changes the checkbox to match the task complete progress we are editing
            completeCB.IsChecked = tasks.Complete;

            this.DataContext = tasks; 
        }

        // Changes the priority based on what radio button we choose
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

        // We hit the cancel button, nuff said
        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
