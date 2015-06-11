using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.WebUI.Controllers
{
    [Authorize]
    public class SubTaskController : Controller
    {
        ISubTaskService _subTaskService;
        public SubTaskController(ISubTaskService subTaskService)
        {
            _subTaskService = subTaskService;
        }
       
        public PartialViewResult _GetForTask(int id)
        {
            var tasks = _subTaskService.GetSubTasksByTaskId(id);
            ViewBag.TaskId = id;

            return PartialView("_GetForTask", tasks);
        }

        [ChildActionOnly]
        public PartialViewResult _SubTaskForm(int taskId)
        {
            var model = new SubTask();

            model.TaskId = taskId;

            return PartialView("_SubTaskForm", model);
        }

        [ChildActionOnly]
        public PartialViewResult _SubTask(SubTask model)
        {
            return PartialView("_SubTask", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddSubTask(SubTask model)
        {
            if (ModelState.IsValid)
            {
                _subTaskService.Add(model);
            }

            var subTasks = _subTaskService.GetSubTasksByTaskId(model.TaskId);
            ViewBag.TaskId = model.TaskId;

            return PartialView("_GetForTask", subTasks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _ChangeSubTaskStatus(SubTask model)
        {
            _subTaskService.ChangeSubTaskStatus(model.Id);
            
            ViewBag.TaskId = model.TaskId;

            var subTasks = _subTaskService.GetSubTasksByTaskId(model.TaskId);

            return PartialView("_GetForTask", subTasks);
        }

        [HttpPost]
        public PartialViewResult _RemoveSubTask(SubTask model)
        {
            _subTaskService.RemoveById(model.Id);

            ViewBag.TaskId = model.TaskId;

            var subTasks = _subTaskService.GetSubTasksByTaskId(model.TaskId);
            return PartialView("_GetForTask", subTasks);
        }
    }
}
