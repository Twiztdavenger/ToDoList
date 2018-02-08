using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;

namespace ToDoList.Database
{
    class Connection
    {
        protected SQLiteConnection mConn;
        protected SQLiteDataAdapter mAdapter;
        protected SQLiteCommand cmd = new SQLiteCommand();

        protected bool isComplete(string str)
        {
            if (str == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected String isComplete(bool bl)
        {
            if (bl)
            {
                return "True";
            }
            else
            {
                return "False";
            }
        }



    }
}
