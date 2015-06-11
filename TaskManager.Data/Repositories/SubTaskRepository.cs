using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Abstract.Repositories;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.Repositories
{
    public class SubTaskRepository : BaseRepository<SubTask>, ISubTaskRepository
    {
        public List<SubTask> GetSubTasksByTaskId(int subTaskId)
        {
            var subTasks = (from st in dbContext.SubTasks
                            orderby st.IsFinished
                            where st.TaskId == subTaskId
                            select st).ToList();

            return subTasks;
        }
    }
}
