using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserTask.Objects.Static;
using UserTask.Objects;
using System.Data;

namespace UserTask.Models
{
    public class TaskList
    {
        public Guid UserGUID { get; protected set; }
        public List<TaskItem> Tasks { get; protected set; }
        private MySQLClass _DB = SingletonObjects.DB;
        public TaskList(Guid guid)
        {
            UserGUID = guid;
            
        }
        public bool Create(TaskItem item)
        {
            item.ItemGUID = Guid.NewGuid().ToString();
            string[] payload = { item.ItemGUID, UserGUID.ToString(), item.TaskName, item.TaskDescription, item.IsDone };
            if (_DB.ExcecuteQuery("INSERT INTO `Tasks` (GUID, User, TaskName, TaskDescription, IsDone) VALUES(@val1, @val2, @val3, @val4, @val5)", payload))
            {
                Tasks.Add(item);
                return true;
            }
            return false;
            
        }
        public void Retrieve()
        {
            string[] payload = { UserGUID.ToString() };
            ExtractFromDataSet(_DB.GetData("SELECT * FROM `Tasks` WHERE User = @val1", payload));
        }
        public bool Update(TaskItem item)
        {
            var task_item = Tasks.FirstOrDefault(r => r.ItemGUID == item.ItemGUID);
            if(task_item != null)
            {
                string[] payload = { item.ItemGUID, UserGUID.ToString(), item.TaskName, item.TaskDescription, item.IsDone };
                if (_DB.ExcecuteQuery("UPDATE `Tasks` SET User = @val2, TaskName = @val3, TaskDescription = @val4, IsDone = @val5 WHERE GUID = @val1", payload))
                {
                    task_item = item;
                    return true;
                }
            }
            return false;
        }
        public bool Delete(string GUID)
        {
            var task_item = Tasks.FirstOrDefault(r => r.ItemGUID == GUID);
            if (task_item != null)
            {
                string[] payload = { GUID };
                if (_DB.ExcecuteQuery("DELETE FROM `Tasks`  WHERE GUID = @val1", payload))
                {
                    Tasks.Remove(task_item);
                    return true;
                }
            }
            return false;
        }
        private void ExtractFromDataSet(DataSet ds)
        {
            Tasks = new List<TaskItem>();
            foreach (DataTable table in ds.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    TaskItem item = new TaskItem();
                    item.ItemGUID = row["GUID"].ToString();
                    item.TaskName = row["TaskName"].ToString();
                    item.TaskDescription = row["TaskDescription"].ToString();
                    item.IsDone = row["IsDone"].ToString();
                    Tasks.Add(item);
                }
            }
        }
    }
}