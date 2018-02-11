using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Data.SQLite;
using System.Windows;
using ToDoList.Database;

namespace ToDoList.ViewModel
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        // This is our official task list
        // This is an Observable Collection so the list will automatically update without us
        // refreshing the list manually
        private ObservableCollection<TaskViewModel> tasks = new ObservableCollection<TaskViewModel>();

        ManageDatabase md = new ManageDatabase();

        public ObservableCollection<TaskViewModel> Tasks {
            get { return tasks; }
            set {
                if (tasks != value)
                {
                    Tasks = value;
                    NotifyPropertyChanged(nameof(Tasks));
                }

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public int ID { get; set; }
        public string TaskName { get; set; }
        public bool Complete { get; set; }
        public int Priority { get; set; }
        public bool Archive { get; set; }


        
        // Constructor that contains all of our relay commands
        public TaskListViewModel()
        {
            DeleteCommand = new RelayCommand(Delete);
            EditCommand = new RelayCommand(Edit);
            CreateCommand = new RelayCommand(Create);
        }


        // DELETE //
        private void Delete(object task)
        {
            if (task != null)
            {
                // This grabs the task object from the Observable Collection and delets it
                this.Tasks.Remove(Tasks.Where(i => i == (TaskViewModel)task).Single());
                md.removeTaskRecord((TaskViewModel)task);
            }
        }

        // EDIT //
        private void Edit(object task)
        {
            if (task != null)
            {
                // We pass the task and this TaskViewModel into the dialog and show it
                var edTask = new TaskDialog((TaskViewModel)task, this);

                edTask.ShowDialog();
            }
        }

        // CREATE //
        private void Create(object obj)
        {

            // If the object we are passing is of type TaskListViewModel
            if (obj is TaskListViewModel taskList)
            {
                

                // If tasklist DOES contain the task we are editing

                // BUG - IF ITS A NEW TASK THE ID IS NOT UNIQUE AND EDITS DIFFERENT TASK

                if (taskList.Tasks.Any(p => p.ID == taskList.ID))
                {
                    // We create a temporary task to hold all of our data
                    TaskViewModel tsk = new TaskViewModel()
                    {
                        ID = taskList.ID,
                        Name = taskList.TaskName,
                        Complete = taskList.Complete,
                        Priority = taskList.Priority,
                        Archive = false
                    };
                    // Grab the task from the ObservableCollection Tasks and assign it to tempTask
                    TaskViewModel tempTask = taskList.Tasks.Single(p => p.ID == taskList.ID);

                    // Grab the index of this tempTask in Tasks Collection
                    int index = taskList.Tasks.IndexOf(tempTask);

                    // Update the task at this index and pass it through the database to update
                    taskList.Tasks[index] = tsk;
                    md.addTaskRecord(tsk);

                    //Resets these values for the next tasklist call
                    resetViewModel();

                } else
                {
                    // We create a temporary task to hold all of our data
                    TaskViewModel tsk = new TaskViewModel()
                    {
                        Name = taskList.TaskName,
                        Complete = taskList.Complete,
                        Priority = taskList.Priority,
                        Archive = false
                    };
                    // If tasklist does NOT contain the task we are editing
                    // We udate the tasks collection with the new task we are adding
                    taskList.Tasks.Add(tsk);
                    md.addTaskRecord(tsk);

                    // Resets these values for the next tasklist call
                    // 
                    resetViewModel();
                }
                
            }
        }

        // COMMANDS 
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }

        //RESET METHOD

        public void resetViewModel()
        {
            //Resets values to avoid weird bug
            this.ID = -99;
            this.TaskName = "";
            this.Complete = false;
            Archive = false;
        }
    }


}
