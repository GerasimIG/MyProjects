using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TaskManager.Domain.Abstract.Repositories;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.Repositories
{
    public class SubTaskRepository : BaseRepository<SubTask>, ISubTaskRepository
    {
        public List<SubTask> GetSubTasksByTaskId(int taskId,string userName)
        {
            var task = dbContext.Tasks.Find(taskId);
            if(task.Category.UserName == userName)
            {
                return task.SubTasks.OrderBy(x => x.IsFinished).ToList();
            }

            throw new NullReferenceException();
        }

        public Task GetTaskById(int id)
        {
            return dbContext.Tasks.Find(id);
        }

        public void UpdateTask(Task task)
        {
            dbContext.Entry(task).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
