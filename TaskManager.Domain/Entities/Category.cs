using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string UserName { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
