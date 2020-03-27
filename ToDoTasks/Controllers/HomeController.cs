using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ToDoTasks.Entities;
using ToDoTasks.Models;

namespace ToDoTasks.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ToDoTaskService _toDoTaskService;
        public HomeController()
        {
            _toDoTaskService = new ToDoTaskService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult ToDoTasks()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetTodoTasks()
        {
            string userId = GetCurrentUser();

            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);

            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0,column]"] + "][name]"];
            string searchDirection = Request["order[0][dir]"];

            var result = _toDoTaskService.GetTodoTasks(userId);
            int recordsTotal = result.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                result = result.Where(i => i.Title.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            int recordsFiltered = result.Count();
            // paging
            result = result.Skip(start).Take(length).ToList();

            return Json(new
            {
                data = result,
                draw = Request["draw"],
                recordsTotal,
                recordsFiltered
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public async Task<JsonResult> SaveTodoTask(ToDoTask toDoTask)
        {
            if (toDoTask.ToDoTaskId == 0)
            {
                toDoTask.UserId = GetCurrentUser();
                toDoTask.IsDone = false;
                toDoTask.Status = Enums.Status.Active;
                var result = await _toDoTaskService.Add(toDoTask);
                return Json(result);
            }
            else
            {
                var task = await _toDoTaskService.GetById(toDoTask.ToDoTaskId);
                task.Title = toDoTask.Title;
                var result = await _toDoTaskService.Save(task);
                return Json(result);
            }
        }

        public async Task<JsonResult> GetToDoTaskById(int taskId)
        {
            var task = await _toDoTaskService.GetById(taskId);
            return Json(task);
        }


        public async Task<JsonResult> DeleteToDoTask(int taskId)
        {
            var task = await _toDoTaskService.GetById(taskId);
            task.Status = Enums.Status.Deleted;
            var result =await _toDoTaskService.Save(task);
            return Json(result);
        }

        public async Task<JsonResult> MarkTaskAsDone(int taskId)
        {
            var task = await _toDoTaskService.GetById(taskId);
            task.IsDone = true;
            var result = await _toDoTaskService.Save(task);
            return Json(result);
        }

        
    }
}