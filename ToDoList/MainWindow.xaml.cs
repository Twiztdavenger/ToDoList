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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoList.ViewModel;

using System.Data.Sql;
using ToDoList.Database;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace ToDoList
{
    /// <summary>
    /// 
    /// This is the main window that will show the list of task items in 
    /// the database.
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        ManageDatabase mD = new ManageDatabase();

        TaskListViewModel tasks = new TaskListViewModel();

        // Contstructor for the main window, decides the data context and syncs
        // data from the database to the task listview

        public MainWindow()
        {
            InitializeComponent();

            tasks = mD.taskDataTransfer(tasks);

            this.DataContext = tasks;
        }

        // This method is called when 'New' is pressed on the menu
        // It shows the task dialog to add a task
        private void newTask(object sender, RoutedEventArgs e)
        {
            var addTask = new TaskDialog(tasks);

            addTask.ShowDialog();

        }

        
    }
}
