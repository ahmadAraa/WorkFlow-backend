using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProjectService
    {
        protected readonly ApplicationDbContext _dbContext;
        public ProjectService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Project AddProject(ProjectVM projectVM,int id)
        {
            var project = new Project
            {
                Name = projectVM.Name,
                Description = projectVM.Description,
                DateCreated = DateTime.Now,
                userId = id
            };

            _dbContext.projects.Add(project);
            _dbContext.SaveChanges();

            return project;
        }
        public async Task<Project> GetProjectById(int? id)
        {
            var projects = _dbContext.projects.FirstOrDefaultAsync(projects => projects.Id == id);
            return await projects;
        }
        public async Task<List<Project>> GetAllProjects()
        {
            var projects = _dbContext.projects.ToListAsync();
            return await projects;
        }
       public void EditProject(int? id, ProjectVM projectVM)
        {
            var project = _dbContext.projects.FirstOrDefault(x => x.Id == id);
            if (project is not null) { 
                project.Name = projectVM.Name;  
            project.Description = projectVM.Description;
                _dbContext.SaveChanges();
            }

         }
        public void DeleteProject(int? id) {
            var project = _dbContext.projects.FirstOrDefault(x => x.Id == id);
            _dbContext.projects.Remove(project);
            _dbContext.SaveChanges();

        }
       
    }
}
