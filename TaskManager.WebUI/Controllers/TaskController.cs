using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Domain.Abstract.Services;

namespace TaskManager.WebUI.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICategoryService _categoryService;
        public TaskController(ITaskService taskService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _taskService = taskService;
        }
        public ActionResult Category(int id = 0)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddTask(Domain.Entities.Task model)
        {
            if (ModelState.IsValid)
            {
                _taskService.Add(model);
            }
            ViewBag.CategoryId = model.CategoryId;

            var tasks = _taskService.GetTasksByCategoryId(model.CategoryId);
              
            return PartialView("_Tasks", tasks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _ChangeTaskStatus(Domain.Entities.Task model)
        {
            _taskService.ChangeTaskStatus(model.Id);

            ViewBag.CategoryId = model.CategoryId;

            var tasks = _taskService.GetTasksByCategoryId(model.CategoryId);
            return PartialView("_Tasks", tasks);
        }
        [ChildActionOnly]
        public PartialViewResult _TaskForm(int categoryId)
        {
            var model = new Domain.Entities.Task();

            model.CategoryId = categoryId;
            
            return PartialView("_TaskForm", model);
        }

        [ChildActionOnly]
        public PartialViewResult _Task(Domain.Entities.Task model)
        {
            return PartialView("_Task", model);
        }

        [ChildActionOnly]
        public PartialViewResult _Tasks(int categoryId)
        {

            try
            {
                if (categoryId == 0)
                {
                    var category = _categoryService.GetAll().FirstOrDefault();
                    if (category != null) categoryId = category.Id;
                }

                if (categoryId == 0)
                {
                    return PartialView("_NoCategories");
                }

                ViewBag.CategoryId = categoryId;
                var tasks = _taskService.GetTasksByCategoryId(categoryId);
                return PartialView("_Tasks", tasks);
            }
            catch (NullReferenceException)
            {
                Response.StatusCode = 404;
                return PartialView("_Error404");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _RemoveTask(Domain.Entities.Task model)
        {
            _taskService.RemoveById(model.Id);

            ViewBag.CategoryId = model.CategoryId;

            var tasks = _taskService.GetTasksByCategoryId(model.CategoryId);
            return PartialView("_Tasks", tasks);
        }

        [HttpPost]
        public PartialViewResult _RemoveFinishedTasks(Domain.Entities.Task model)
        {
            
            _taskService.RemoveFinisedTasksByCategoryId(model.CategoryId);
            ViewBag.CategoryId = model.CategoryId;
            var tasks = _taskService.GetTasksByCategoryId(model.CategoryId);
            return PartialView("_Tasks", tasks);
        }

        [ChildActionOnly]
        public PartialViewResult _RemoveFinishedTasksForm(Domain.Entities.Task model)
        {
            return PartialView("_RemoveFinishedTasksForm", model);
        }
    }
}
