using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter.Project.TodoList.Models
{
    class TodoTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsComplete { get; set; }


        public TodoTask(int id, string derscription)
        {
            Id = id;
            Description = derscription;    
            CreatedDate = DateTime.Now;
            IsComplete = false;
        }
    }
}
