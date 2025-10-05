using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus TaskStatus { get; set; } = TaskStatus.Todo;
        public DateTime DateAdded { get; set; }
        public DateTime? DueDate { get; set; }

        public int ProjectId { get; set; } 
        public Project Project { get; set; }
    }
}