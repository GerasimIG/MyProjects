using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Abstract.Services
{
    public interface ISubTaskService : IBaseService<SubTask>
    {
        List<SubTask> GetSubTasksByTaskId(int subTaskId);
        void ChangeSubTaskStatus(int subTaskId);
    }
}

