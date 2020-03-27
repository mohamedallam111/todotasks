using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ToDoTasks.Entities;
using ToDoTasks.Models;

namespace ToDoTasks
{
    public class ToDoTaskService
    {
        
        public async Task<ToDoTask> Add(ToDoTask toDoTask)
        {
            using (var dc=new ApplicationDbContext())
            {
                dc.ToDoTasks.Add(toDoTask);
                await dc.SaveChangesAsync();
            }
            return toDoTask;
        }

        public async Task<ToDoTask> Save(ToDoTask toDoTask)
        {
            using (var dc = new ApplicationDbContext())
            {
                dc.Entry(toDoTask).State = EntityState.Modified;
                await dc.SaveChangesAsync();
            }
            return toDoTask;
        }

        public async Task<ToDoTask> GetById(int taskId)
        {
            using (var dc=new ApplicationDbContext())
            {
                return await dc.ToDoTasks.FirstOrDefaultAsync(i => i.ToDoTaskId == taskId);
            }
        }

        public IEnumerable<ToDoTask> GetTodoTasks(string userId)
        {
            using (var dc = new ApplicationDbContext())
            {
                return dc.ToDoTasks
                    .Where(i => i.UserId == userId)
                    .Where(i => i.Status == Enums.Status.Active)
                    .ToList();
            }
        }
    }
}