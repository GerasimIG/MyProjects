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
    [RoutePrefix("api/sub-tasks")]
    public class SubTasksController : ApiController
    {      
        private readonly ISubTaskService _subTaskService;

        public SubTasksController(ISubTaskService subTaskService)
        {
            _subTaskService = subTaskService;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostSubTask(SubTask model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _subTaskService.Add(model);
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
        public HttpResponseMessage DeleteSubTask(int id)
        {      
            try
            {
                _subTaskService.RemoveById(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage PutSubTask(SubTask model)
        {        
            try
            {
                _subTaskService.ChangeSubTaskStatus(model.Id);
                return Request.CreateResponse(HttpStatusCode.OK, "Ok");
            }
            catch (BadRequestException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}