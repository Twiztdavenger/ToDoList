using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.ViewModel;

namespace ToDoList.Database
{
    class ManageDatabase : Connection
    {
        String cmdText;
        

        public ManageDatabase()
        {
            string mDbPath = AppDomain.CurrentDomain.BaseDirectory + "/Data/todolist.db";

            mConn = new SQLiteConnection("Data Source=" + mDbPath);

            cmd.Connection = mConn;

        }

        public TaskListViewModel taskDataTransfer(TaskListViewModel tasks)
        {
            DataTable mTable;
            //Connection Stuff
            string mDbPath = AppDomain.CurrentDomain.BaseDirectory + "/Data/todolist.db";

            mConn = new SQLiteConnection("Data Source=" + mDbPath);

            mConn.Open();

            mAdapter = new SQLiteDataAdapter("SELECT * FROM [tasks]", mConn);
            mTable = new DataTable(); // Don't forget initialize!
            mAdapter.Fill(mTable);

            foreach (DataRow row in mTable.Rows)
            {
                tasks.Tasks.Add(new TaskViewModel() { ID = Convert.ToInt32(row["ID"]), Name = row["Name"].ToString(), Complete = isComplete(row["Complete"].ToString()), Priority = Convert.ToInt32(row["Priority"]) });
            }

            return tasks;
        }

        public void addTaskRecord(TaskViewModel task)
        {
            
            // I could not get this to work parameterized for some reason
            DataTable mTable;
            cmdText = "SELECT * from tasks where ID = " + task.ID;

            cmd.CommandText = cmdText;
            //SQLiteParameter c1 = new SQLiteParameter("ID", task.ID);

            mConn.Open();

            //cmd.Parameters.Add(c1);
            mAdapter = new SQLiteDataAdapter(cmd.CommandText, mConn);

            mTable = new DataTable();

            mAdapter.Fill(mTable);


            // If the database already contains task, update it
            if(mTable.Rows.Count >= 1)
            {
                cmdText = "Update tasks set NAME = @name, COMPLETE = @comp, PRIORITY = @pr, ARCHIVE = @ar WHERE ID = @ID";

                cmd.CommandText = cmdText;

                SQLiteParameter pp1 = new SQLiteParameter("name", task.Name);
                SQLiteParameter pp2 = new SQLiteParameter("comp", task.Complete);
                SQLiteParameter pp3 = new SQLiteParameter("pr", task.Priority);
                SQLiteParameter pp4 = new SQLiteParameter("ar", task.Archive);
                SQLiteParameter pp5 = new SQLiteParameter("ID", task.ID);

                cmd.Parameters.Add(pp1);
                cmd.Parameters.Add(pp2);
                cmd.Parameters.Add(pp3);
                cmd.Parameters.Add(pp4);
                cmd.Parameters.Add(pp5);


                cmd.ExecuteNonQuery();
                

            } else // If it is a new task entirely, add it to the database instead
            {
                cmdText = "Insert into tasks (NAME, COMPLETE, PRIORITY, ARCHIVE) Values(@name, @comp, @pr, @ar)";

                cmd.CommandText = cmdText;

                SQLiteParameter p1 = new SQLiteParameter("name", task.Name);
                SQLiteParameter p2 = new SQLiteParameter("comp", task.Complete);
                SQLiteParameter p3 = new SQLiteParameter("pr", task.Priority);
                SQLiteParameter p4 = new SQLiteParameter("ar", task.Archive);

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);

                
                cmd.ExecuteNonQuery();
                
            }
            mConn.Close();
        }

        

        public void removeTaskRecord(TaskViewModel task)
        {
            cmdText = "DELETE from tasks WHERE ID = @ID";

            cmd.CommandText = cmdText;

            SQLiteParameter p1 = new SQLiteParameter("ID", task.ID);

            cmd.Parameters.Add(p1);

            mConn.Open();
            cmd.ExecuteNonQuery();
            mConn.Close();
        }


        
    }
}
