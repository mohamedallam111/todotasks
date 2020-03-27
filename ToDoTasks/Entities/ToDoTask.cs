using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ToDoTasks.Enums;
using ToDoTasks.Models;

namespace ToDoTasks.Entities
{
    public class ToDoTask
    {
        [Key]
        public int ToDoTaskId { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        [NotMapped]
        public string strDueDate { get { return DueDate.ToString("MM/dd/yyyy"); }  }
        public bool IsDone { get; set; }
        public Status Status { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}