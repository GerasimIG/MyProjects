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
        List<Task> GetTasksByCategoryId(int categoryId, string userName);
        void ChangeTaskStatus(int taskId);
        void RemoveFinishedTasksByCategoryId(int categoryId);
    }
}
