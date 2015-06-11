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
        public List<Task> GetTasksByCategoryId(int categoryId, string userName)
        {   

            var category = dbContext.Categories.Find(categoryId);

            if(category != null)
            {
                if(category.UserName == userName)
                {
                    return category.Tasks.OrderBy(x => x.IsFinished).ToList();
                }
            }

            throw new NullReferenceException();
        }


        public void RemoveFinishedTasksByCategoryId(int categoryId)
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
