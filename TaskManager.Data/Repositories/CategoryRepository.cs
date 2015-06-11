using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Abstract.Repositories;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public List<Category> GetCategoriesByUserName(string userName)
        {
            var categories = (from c in dbContext.Categories
                              where c.UserName == userName
                              select c).ToList();

            return categories;
        }
    }
}