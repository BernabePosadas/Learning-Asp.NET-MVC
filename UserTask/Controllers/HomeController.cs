using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserTask.Models;
using System.Text;
using UserTask.Objects.Static;

namespace UserTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
               return View();
            }
            return Redirect("~/UserRegistration/Login");
        }
        [HttpPost]
        public JsonResult Login(LoginRequestBody body)
        {
            User user = new User();
            ResponseTemplate response = null;
            if (!user.CheckIfEmailExist(body.Email))
            {
                response = new ResponseTemplate
                {
                    Status = "404",
                    Message = "No User is registered with the email given."
                };
                return Json(response);
            }
            user.RetrieveUserByEmail(body.Email);
            if (SingletonObjects.Hasher.CompareHash(user.Password.ToString(), Encoding.UTF8.GetBytes(body.Password.ToString()))){
                response = new ResponseTemplate
                {
                    Status = "200",
                    Message = "Success"
                };
                Session.Add("User", user.UserGUID.ToString());
            }
            else
            {
                response = new ResponseTemplate
                {
                    Status = "400",
                    Message = "Password is Incorrect"
                };
            }
            return Json(response);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("~/UserRegistration/Login");
        }
        public ActionResult TaskList()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            return Redirect("~/UserRegistration/Register");
        }
        [HttpPost]
        public JsonResult Register(RegisterRequestBody body)
        {
            User user = new User();
            ResponseTemplate response = null;
            if (user.CheckIfEmailExist(body.Email))
            {
                response = new ResponseTemplate
                {
                    Status = "400",
                    Message = "There is an existing user with the given email."
                };
                return Json(response);
            }
            string hash1 = SingletonObjects.Hasher.GenerateHash(Encoding.UTF8.GetBytes(body.Password));
            string hash2 = SingletonObjects.Hasher.GenerateHash(Encoding.UTF8.GetBytes(body.ConfirmPassword));
            if(!SingletonObjects.Hasher.CompareHash(hash1, hash2))
            {
                response = new ResponseTemplate
                {
                    Status = "400",
                    Message = "Password does not match."
                };
                return Json(response);
            }
            body.Password = hash1;
            user.Save(body);
            response = new ResponseTemplate
            {
                Status = "200",
                Message = "Success"
            };
            Session.Add("User", user.UserGUID.ToString());
            return Json(response);
        }
    }
}
