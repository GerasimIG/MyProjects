using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public bool IsFinished { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<SubTask> SubTasks { get; set; }
    }
}
