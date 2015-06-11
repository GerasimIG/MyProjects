using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Abstract.Repositories;

namespace TaskManager.Domain.Concrete.Services
{
    public class TaskService : BaseService<Task>, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
            : base(taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public List<Task> GetTasksByCategoryId(int categoryId)
        {
            return _taskRepository.GetTasksByCategoryId(categoryId);
        }

        public void ChangeTaskStatus(int taskId)
        {
            var task = _taskRepository.GetById(taskId);

            task.IsFinished = !task.IsFinished;

            if (task.SubTasks != null)
            {
                    foreach(var element in task.SubTasks)
                    {
                        element.IsFinished = task.IsFinished;
                    }   
            }
            _taskRepository.Update(task);
        }


        public void RemoveFinisedTasksByCategoryId(int categoryId)
        {
            _taskRepository.RemoveFinisedTasksByCategoryId(categoryId);
        }
    }
}
