using System.Collections.Generic;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Abstract.Repositories
{
    public interface ISubTaskRepository : IBaseRepository<SubTask>
    {
        List<SubTask> GetSubTasksByTaskId(int taskId,string userName);
        Task GetTaskById(int id);
        void UpdateTask(Task task);
    }
}
