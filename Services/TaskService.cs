using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TaskService
    {
    private readonly ApplicationDbContext _dbContext;
        public TaskService(ApplicationDbContext dbContext)
        {
            
            _dbContext = dbContext; 
        }
        public void AddTask(ActivityVM tasks)
        {
            var task = new Models.Activity()
            {
                Name = tasks.Name,
                Description = tasks.Description,
                DueDate = tasks.DueDate,
                TaskStatus = tasks.TaskStatus,
                DateAdded = DateTime.Now
            };
            _dbContext.tasks.Add(task);
            _dbContext.SaveChanges();
        }
        public void RemoveTask(int?id) { 
        var task = _dbContext.tasks.FirstOrDefault(t => t.Id == id);
            if (task != null) { 

            _dbContext.tasks.Remove(task);
            }
            _dbContext.SaveChanges();

        }
        public async Task<Activity?> GetTaskById(int id)
        {
            return await _dbContext.tasks.FindAsync(id);
        }
        public async Task<List<Models.Activity>> GetAllTasks()
        {
           return await _dbContext.tasks.ToListAsync();
            
            
        }
        public async Task<Models.Activity> EditTask(int? id,ActivityVM tasksVM)
        {
            var task =  _dbContext.tasks.FirstOrDefault(task => task.Id == id);
            if (task is not null) { 
                task.Name = tasksVM.Name;
                task.Description = tasksVM.Description;
                task.TaskStatus = tasksVM.TaskStatus;
                task.DueDate = tasksVM.DueDate;
                
                _dbContext.SaveChanges();

            }
            return task;
         

        }
        public async Task<int> RemoveAllTasks()
        {
        var allTasks = await _dbContext.tasks.ToListAsync();
        _dbContext.tasks.RemoveRange(allTasks);
            return await _dbContext.SaveChangesAsync();

        }





    }
}
