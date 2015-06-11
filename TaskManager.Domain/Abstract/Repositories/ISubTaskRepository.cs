﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Abstract.Repositories
{
    public interface ISubTaskRepository : IBaseRepository<SubTask>
    {
        List<SubTask> GetSubTasksByTaskId(int taskId,string userName);
    }
}
