using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Domain.Entities
{
    public class SubTask
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsFinished { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
