using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserTask.Models
{
    public class RegisterRequestBody : UserParameters
    {
        public string ConfirmPassword { get; set; }
    }
}