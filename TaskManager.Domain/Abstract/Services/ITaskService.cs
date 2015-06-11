using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Abstract.Services
{
    public interface ITaskService: IBaseService<Task>
    {
        List<Task> GetTasksByCategoryId(int categoryId);
        void ChangeTaskStatus(int taskId);
        void RemoveFinisedTasksByCategoryId(int categoryId);
    }
}
