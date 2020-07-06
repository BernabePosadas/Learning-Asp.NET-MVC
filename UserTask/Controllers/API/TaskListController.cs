using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserTask.Models;

namespace UserTask.Controllers
{
    public class TaskListController : Controller {

        [HttpPost]
        public ActionResult Create(TaskItem item)
        {
            User user = new User();
            ResponseTemplate response;
            if (Session["User"] == null)
            {
                response = new ResponseTemplate
                {
                    Status = "401",
                    Message = "Unauthorize access."
                };
                return Json(response);
            }
            user.RetrieveUserByGUID(Session["User"].ToString());
            if (!user.UserTask.Create(item))
            {
                response = new ResponseTemplate
                {
                    Status = "500",
                    Message = "Unable to create new task"
                };
                return Json(response);
            }
            response = new ResponseTemplate
            {
                Status = "200",
                Message = "Success.",
                Payload = user.UserTask.Tasks
            };
            return Json(response);
        }
        [HttpPost]
        public ActionResult List()
        {

            User user = new User();
            ResponseTemplate response = null;
            if (Session["User"] == null)
            {
                response = new ResponseTemplate
                {
                    Status = "401",
                    Message = "Unauthorize access."
                };
                return Json(response);
            }
            user.RetrieveUserByGUID(Session["User"].ToString());
            return Json(user.UserTask.Tasks);
        }
        [HttpPost]
        public ActionResult Update(TaskItem item)
        {
            User user = new User();
            ResponseTemplate response = null;
            if (Session["User"] == null)
            {
                response = new ResponseTemplate
                {
                    Status = "401",
                    Message = "Unauthorize access."
                };
                return Json(response);
            }
            user.RetrieveUserByGUID(Session["User"].ToString());
            if (!user.UserTask.Update(item))
            {
                response = new ResponseTemplate
                {
                    Status = "500",
                    Message = "Unable to save task state."
                };
                return Json(response);
            }
            user.UserTask.Retrieve(); // refresh user task
            response = new ResponseTemplate
            {
                Status = "200",
                Message = "Success.",
                Payload = user.UserTask.Tasks
            };
            return Json(response);
        }
        [HttpDelete]
        public ActionResult Delete(string GUID)
        {
            User user = new User();
            ResponseTemplate response = null;
            if (Session["User"] == null)
            {
                response = new ResponseTemplate
                {
                    Status = "401",
                    Message = "Unauthorize access."
                };
                return Json(response);
            }
            user.RetrieveUserByGUID(Session["User"].ToString());
            if (!user.UserTask.Delete(GUID))
            {
                response = new ResponseTemplate
                {
                    Status = "500",
                    Message = "Unable to delete task"
                };
                return Json(response);
            }
            response = new ResponseTemplate
            {
                Status = "200",
                Message = "Success.",
                Payload = user.UserTask.Tasks
            };
            return Json(response);
        }
    }
}