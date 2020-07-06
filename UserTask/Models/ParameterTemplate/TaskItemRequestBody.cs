using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserTask.Models
{
    public class TaskItemRequestBody
    {
        public string ItemGUID { get; set; }
        public string IsDone { get; set; }
    }
}