using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserTask.Controllers
{
    public class UserRegistrationController : Controller
    {
        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                return Redirect("~/Home");
            }
            return View("Login");
        }

        public ActionResult Register()
        {
            if (Session["User"] != null)
            {
                return Redirect("~/Home");
            }
            return View("Register");
        }
    }
}