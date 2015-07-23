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


            subTask.Task.IsFinished = subTask.Task.SubTasks.FirstOrDefault(x => !x.IsFinished) == null;
            
            
            _subTaskRepository.Update(subTask);

        }

        public new void Add(SubTask subTask)
        {
            base.Add(subTask);
            var task = _subTaskRepository.GetTaskById(subTask.TaskId);
            task.IsFinished = false;
            _subTaskRepository.UpdateTask(task);
        }

        public new void RemoveById(int id)
        {
            SubTask subTask = _subTaskRepository.GetById(id);
            Task task = subTask.Task;
            

             if (task.SubTasks.Count > 1 && 
                 task.SubTasks.FirstOrDefault(x => !x.IsFinished && x.Id != id) == null)
             {
                 task.IsFinished = true;
             }

             _subTaskRepository.Remove(subTask);
        }
    }
}
