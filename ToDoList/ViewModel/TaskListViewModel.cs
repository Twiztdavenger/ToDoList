using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Command;

using System.Data.SQLite;
using System.Windows;
using ToDoList.Database;

namespace ToDoList.ViewModel
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
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
        public int ID { get; set; }
        public string TaskName { get; set; }
        public bool Complete { get; set; }
        public int Priority { get; set; }
        public bool Archive { get; set; }




        // COMMANDS #######

        public ICommand CreateTaskCommand { get { return new CreateTaskCommand(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }


        //Gather which task was selected
        public TaskViewModel selectedTask;
        public TaskViewModel SelectedTask
        {
            get
            {
                return selectedTask;
            }
            set
            {
                NotifyPropertyChanged("SelectedTask");
                selectedTask = value;
            }
        }

        private void Delete(object task)
        {
            if (task != null)
            {
                this.Tasks.Remove(Tasks.Where(i => i == (TaskViewModel)task).Single());
                md.removeTaskRecord((TaskViewModel)task);
            }
        }

        private void Edit(object task)
        {
            if (task != null)
            {
                var edTask = new TaskDialog((TaskViewModel)task, this);



                edTask.ShowDialog();
            }
        }

        private void Create(object obj)
        {
            if (obj is TaskListViewModel taskList)
            {
                
                TaskViewModel tsk = new TaskViewModel()
                {
                    ID = taskList.ID,
                    Name = taskList.TaskName,
                    Complete = taskList.Complete,
                    Priority = taskList.Priority,
                    Archive = false
                };

                if(taskList.Tasks.Any(p => p.ID == taskList.ID))
                {
                    //If tasklist DOES contain the task we are editing
                    TaskViewModel tempTask = taskList.Tasks.Single(p => p.ID == taskList.ID);
                    int index = taskList.Tasks.IndexOf(tempTask);

                    taskList.Tasks[index] = tsk;
                    md.addTaskRecord(tsk);

                } else
                {
                    taskList.Tasks.Add(tsk);

                    md.addTaskRecord(tsk);
                }
                
            }
        }

        public TaskListViewModel()
        {
            DeleteCommand = new RelayCommand(Delete);
            EditCommand = new RelayCommand(Edit);
            CreateCommand = new RelayCommand(Create);
        }


}


}
