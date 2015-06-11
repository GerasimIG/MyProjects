using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Abstract.Repositories;

namespace TaskManager.Domain.Concrete.Services
{
    public class SubTaskService : BaseService<SubTask>, ISubTaskService
    {
        private readonly ISubTaskRepository _subTaskRepository;
        public SubTaskService(ISubTaskRepository subTaskRepository)
            : base(subTaskRepository)
        {
            _subTaskRepository = subTaskRepository;
        }


        public List<SubTask> GetSubTasksByTaskId(int taskId,string userName)
        {
            return _subTaskRepository.GetSubTasksByTaskId(taskId,userName);
        }


        public void ChangeSubTaskStatus(int subTaskId)
        {
            var subTask = _subTaskRepository.GetById(subTaskId);

            subTask.IsFinished = !subTask.IsFinished;
            _subTaskRepository.Update(subTask);
        }
    }
}
