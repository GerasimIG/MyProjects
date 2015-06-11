using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Abstract.Services
{
    public interface ICategoryService : IBaseService<Category>
    {
        List<Category> GetCategoriesByUserName(string userName);
    }
}
