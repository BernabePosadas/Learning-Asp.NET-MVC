using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserTask.Models
{
    public class ResponseTemplate
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Payload { get; set; }
    }
}