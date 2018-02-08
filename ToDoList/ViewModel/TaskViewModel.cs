using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Command;

namespace ToDoList.ViewModel
{
    public class TaskViewModel : TaskListViewModel
    {
        public ICommand EditTaskCommand { get { return new EditTaskCommand(); } }

        public int ID { get; set; }
        public String Name { get; set; }
        public bool Complete { get; set; }

        public int Priority { get; set; }

        public bool Archive { get; set; }

        public double iconOpacity;
        public double IconOpacity
        {
            get
            {
                if (Priority == 3)
                {
                    return 1;
                }
                else if (Priority == 2)
                {
                    return .5;
                }
                else
                {
                    return  0;
                }
            }
        }



    }
}
