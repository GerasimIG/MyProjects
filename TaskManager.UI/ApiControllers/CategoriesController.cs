using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Data.Repositories;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Concrete.Services;
using TaskManager.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace TaskManager.UI.ApiControllers
{
    [Authorize(Roles = "ApprovedMembers")]
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly ITaskService _taskService;

        public CategoriesController(ITaskService taskService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _taskService = taskService;

        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCategories()
        {
           
            try
            {
                var categories = _categoryService.GetCategoriesByUserName(User.Identity.Name);
                return Request.CreateResponse(HttpStatusCode.OK,categories);
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostCategory(Category model)
        {
            try
            {
                if (!String.IsNullOrEmpty(model.Text))
                {
                    model.UserName = User.Identity.Name;
                    _categoryService.Add(model);
                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid model state");
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteCategory(int id)
        {
            try
            {
                _categoryService.RemoveById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
        }

        [HttpDelete]
        [Route("{id}/tasks")]
        public HttpResponseMessage DeleteAllFinishedTasksInCategory(int id)
        {
            try
            {
                _taskService.RemoveFinishedTasksByCategoryId(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }           
        }

        [HttpGet]
        [Route("{id}/tasks")]
        public HttpResponseMessage GetCategoryTasks(int id)
        {
            try
            {
                var categoryTasks = _taskService.GetTasksByCategoryId(id, User.Identity.Name);
                return Request.CreateResponse(HttpStatusCode.OK, categoryTasks);
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
