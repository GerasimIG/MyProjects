using System.Collections.Generic;
using System.IdentityModel;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Data.Repositories;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Concrete.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.UI.ApiControllers
{
    [Authorize(Roles = "ApprovedMembers")]
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private readonly ITaskService _taskService;
        private readonly ISubTaskService _subTaskService;

        public TasksController(ITaskService taskService, ISubTaskService subTaskService)
        {
            _taskService = taskService;
            _subTaskService = subTaskService;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostTask(Task model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.Add(model);
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
        public HttpResponseMessage DeleteTask(int id)
        {
            try
            {
                _taskService.RemoveById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }         
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetTaskSubTasks(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, 
                    _subTaskService.GetSubTasksByTaskId(id,"John"));
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage PutTask(Task model)
        {
            try
            {
                _taskService.ChangeTaskStatus(model.Id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }       
        }
    }
}
