using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Abstract.Services;
using TaskManager.Domain.Abstract.Repositories;

namespace TaskManager.Domain.Concrete.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
            : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetCategoriesByUserName(string userName)
        {
            return _categoryRepository.GetCategoriesByUserName(userName);
        }
    }
}
