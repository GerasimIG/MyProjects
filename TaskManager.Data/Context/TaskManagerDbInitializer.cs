using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskManager.Domain.Entities;

namespace TaskManager.Data.Context
{
    class TaskManagerDbInitializer : CreateDatabaseIfNotExists<TaskManagerDbContext>
    {
        protected override void Seed(TaskManagerDbContext context)
        {
            List<Category> categories = new List<Category>
            {
                new Category { Text = "Home", UserName = "John"},
                new Category { Text = "Work", UserName = "John"},
            };

            foreach (var item in categories)
            {
                context.Categories.Add(item);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
