using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDoList.ViewModel
{
    public class TaskViewModel : TaskListViewModel
    {

        /// <summary>
        /// This is the ViewModel for each individual task, it implements the 
        /// TaskListVViewModel
        /// 
        /// Each task has ID, Name, Complete, Priority, and Archive attributes
        /// 
        /// This class also takes the iconOpacity of the "warning" icon 
        /// and adjusts it based on the priority level
        /// 
        /// </summary>

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
