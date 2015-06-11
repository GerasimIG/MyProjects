using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Abstract.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        List<Category> GetCategoriesByUserName(string userName);
    }
}
