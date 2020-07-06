using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserTask.Objects.Static;
using UserTask.Objects;
using System.Data;

namespace UserTask.Models
{
    public class User : UserParameters
    {
        public Guid UserGUID { get; protected set; }
        public TaskList UserTask { get; protected set; }

        private MySQLClass _DB = SingletonObjects.DB; 

        public bool Save(UserParameters data)
        {
            if (UserGUID != null)
            {
                if (!CheckIfEmailExist(data.Email))
                {
                    return CreateNewUser(data);
                }
                throw new Exception("Email Already Exist!!");
            }
            return false;
        }
        public bool CheckIfEmailExist(string email)
        {
            string[] payload = { email };
            DataTable output = _DB.GetData("SELECT * FROM Users WHERE Email = @val1", payload).Tables[0];
            if (output.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }
        private bool CheckIfGUIDExists()
        {
            string[] payload = { UserGUID.ToString() };
            DataTable output = _DB.GetData("SELECT * FROM Users WHERE GUID = @val1", payload).Tables[0];
            if(output.Rows.Count != 0)
            {
                return true;
            }
            return false;
        }
        private bool CreateNewUser(UserParameters data)
        {
            UserGUID = Guid.NewGuid();
            while (CheckIfGUIDExists())
            {
               UserGUID = Guid.NewGuid();
            }
            FirstName = data.FirstName;
            LastName = data.LastName;
            Email = data.Email;
            Password = data.Password;
            UserTask = new TaskList(UserGUID);
            UserTask.Retrieve();
            string[] payload = { UserGUID.ToString(), FirstName, LastName, Email, Password };
            return _DB.ExcecuteQuery("INSERT INTO `Users` (GUID, FirstName, LastName, Email, Password) VALUES(@val1, @val2, @val3, @val4, @val5)", payload);
        }
        public bool RetrieveUserByGUID(string GUID)
        {
            UserGUID = new Guid(GUID);
            if (CheckIfGUIDExists())
            {
                string[] payload = { UserGUID.ToString() };
                ExtractFromDataSet(_DB.GetData("SELECT * FROM Users WHERE GUID = @val1", payload));
                UserTask = new TaskList(UserGUID);
                UserTask.Retrieve();
                return true;
            }
            return false;
        }
        public bool RetrieveUserByEmail(string email)
        {
            if (CheckIfEmailExist(email))
            {
                string[] payload = { email };
                ExtractFromDataSet(_DB.GetData("SELECT * FROM Users WHERE Email = @val1", payload));
                UserTask = new TaskList(UserGUID);
                UserTask.Retrieve();
                return true;
            }
            return false;
        }
        private void ExtractFromDataSet(DataSet ds)
        {
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    FirstName = row["FirstName"].ToString();
                    LastName = row["LastName"].ToString();
                    UserGUID = new Guid(row["GUID"].ToString());
                    Email = row["Email"].ToString();
                    Password = row["Password"].ToString();
                }
            }
        }
    }
}