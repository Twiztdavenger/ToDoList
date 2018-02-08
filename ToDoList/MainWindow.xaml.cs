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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ManageDatabase mD = new ManageDatabase();

        TaskListViewModel tasks = new TaskListViewModel();

        public MainWindow()
        {
            InitializeComponent();

            tasks = mD.taskDataTransfer(tasks);

            this.DataContext = tasks;
        }

        private void newTask(object sender, RoutedEventArgs e)
        {
            var addTask = new TaskDialog(tasks);

            addTask.ShowDialog();

        }

        
    }
}
