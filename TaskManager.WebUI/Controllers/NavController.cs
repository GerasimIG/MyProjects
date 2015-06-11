using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Abstract.Services;

namespace TaskManager.WebUI.Controllers
{
    [Authorize]
    public class NavController : Controller
    {
        private readonly ICategoryService _categoryService;

        public NavController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [ChildActionOnly]
        public PartialViewResult _Categories()
        {
            var categories = _categoryService.GetCategoriesByUserName(User.Identity.Name);
            return PartialView("_Categories", categories);
        }
        [ChildActionOnly]
        public PartialViewResult _CategoryForm()
        {
            var model = new Category();
            return PartialView("_CategoryForm", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _AddCategory(Category model)
        {
            if (!String.IsNullOrEmpty(model.Text))
            {
                model.UserName = User.Identity.Name;
                _categoryService.Add(model);
            }
            var categories = _categoryService.GetCategoriesByUserName(User.Identity.Name);
            return PartialView("_Categories", categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _RemoveCategory(Category model)
        {
            _categoryService.RemoveById(model.Id);

            return RedirectToAction("Category", "Task");
        }

        [ChildActionOnly]
        public PartialViewResult _Category(Category model)
        {
            return PartialView("_Category", model);
        }
    }
}
