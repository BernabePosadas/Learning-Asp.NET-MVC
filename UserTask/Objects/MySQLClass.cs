using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace UserTask.Objects
{
    public class MySQLClass
    {
        MySqlConnection Connection;
        public MySqlCommand Command = new MySqlCommand();
        public MySqlDataAdapter Adapter = new MySqlDataAdapter();
        public MySqlDataReader Reader { get; set; }
        public string ObjectMessage = "";
        public MySQLClass(string conn)
        {
            Connection = new MySqlConnection(conn);
        }
        public DataSet GetData(string query, string[] data)
        {
            try
            {
                this.PrepareSQL(query, data);
                Adapter = new MySqlDataAdapter(Command);
                DataSet ds = new DataSet();
                Adapter.Fill(ds);
                Connection.Close();
                return ds;
            }
            catch
            {
                throw new Exception("Datagrid filling fail to execute");
            }
           
        }

        public bool ExcecuteQuery(string query, string[] data)
        {
            bool reply = false;
            try
            {
                this.PrepareSQL(query, data);
                Reader = Command.ExecuteReader(); // creates MySQLDataReader instance
                reply = true;
            }
            catch
            {
                ObjectMessage = "SQL command failed to execute";
                reply = false;
            }
            return reply;
        }
        public bool ConnectionTest()
        {
            try
            {
                this.HandleConnection(); // to safekeep if connection is still open
                Connection.Open();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool IsOpen()
        {
            return (Connection.State == ConnectionState.Open);
        }
        private void HandleConnection()
        {
            if (this.IsOpen())
            {
                Connection.Close();
            }
        }
        private void PrepareSQL(string query, string[] data)
        {
            this.HandleConnection();
            Connection.Open();
            Command = new MySqlCommand(query, this.Connection);
            int prefix = 1;
            foreach (string value in data)
            {
                Command.Parameters.AddWithValue(string.Format("@val{0}", prefix), value);
                prefix++;
            }
            Command.Prepare();
        }
    }
}
