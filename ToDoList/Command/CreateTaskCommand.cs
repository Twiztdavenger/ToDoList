﻿using System;
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
    public class CreateTaskCommand : ICommand 
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
            if (parameter is TaskListViewModel taskList)
            {
                TaskViewModel tsk = new TaskViewModel() { ID = taskList.ID, Name = taskList.TaskName,
                    Complete = taskList.Complete, Priority = taskList.Priority,
                    Archive = false};
                taskList.Tasks.Add(tsk);
                
                md.addTaskRecord(tsk);
            }
        }
    }
}
