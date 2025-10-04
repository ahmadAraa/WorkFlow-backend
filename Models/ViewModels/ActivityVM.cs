using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ActivityVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus TaskStatus { get; set; } = TaskStatus.Todo;
        public DateTime? DueDate { get; set; }


    }
}
