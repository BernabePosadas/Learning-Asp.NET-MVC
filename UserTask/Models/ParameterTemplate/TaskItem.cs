using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserTask.Models
{
    public class TaskItem
    {
        public string ItemGUID { get; set; }
        public string TaskName { get;  set; }
        public string TaskDescription { get; set; }

        public string IsDone { get; set; }
    }
}