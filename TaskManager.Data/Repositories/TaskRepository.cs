using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Abstract.Repositories;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.Repositories
{
    public class TaskRepository : BaseRepository<Task>,ITaskRepository
    {
        public List<Task> GetTasksByCategoryId(int categoryId)
        {   
            if(dbContext.Categories.Find(categoryId) == null)
            {
                throw new NullReferenceException();
            }

            var tasks = (from t in dbContext.Tasks
                         where t.CategoryId == categoryId
                         orderby t.IsFinished
                         select t).ToList();

            return tasks;
        }


        public void RemoveFinisedTasksByCategoryId(int categoryId)
        {
            var tasks = (from c in dbContext.Tasks
                              where c.IsFinished
                              select c).ToList();

            foreach(var element in tasks)
            {
                Remove(element);
            }
        }
    }
}
