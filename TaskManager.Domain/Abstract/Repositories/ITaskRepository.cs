using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Abstract.Repositories
{
    public interface ITaskRepository : IBaseRepository<Task>
    {
        List<Task> GetTasksByCategoryId(int categoryId);
        void RemoveFinisedTasksByCategoryId(int categoryId);
    }
}
