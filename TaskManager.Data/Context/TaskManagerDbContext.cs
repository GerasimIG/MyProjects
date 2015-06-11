using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TaskManager.Domain.Entities;


namespace TaskManager.Data.Context
{
    public class TaskManagerDbContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubTask> SubTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(true);
            
            modelBuilder.Entity<Task>()
                .HasMany(e => e.SubTasks)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(true);
        }
    }
}

