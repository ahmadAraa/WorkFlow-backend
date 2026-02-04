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
        protected readonly IRepositoryInterface<Project> _projectRepo;
        public ProjectService(IRepositoryInterface<Project> projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<Project> AddProject(ProjectVM projectVM, int userId)
        {
            var project = new Project
            {
                Name = projectVM.Name,
                Description = projectVM.Description,
                DateCreated = DateTime.Now,
                userId = userId
            };

            await _projectRepo.AddAsync(project);
            await _projectRepo.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> GetProjectById(int id)
        {
            return await _projectRepo.GetByIdAsync(id);
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var projects = await _projectRepo.GetAllAsync();
            return projects.ToList();
        }

        public async Task EditProject(int id, ProjectVM projectVM)
        {
            var project = await _projectRepo.GetByIdAsync(id);
            if (project is not null)
            {
                project.Name = projectVM.Name;
                project.Description = projectVM.Description;
                await _projectRepo.UpdateAsync(project);
                await _projectRepo.SaveChangesAsync();
            }
        }

        public async Task DeleteProject(int id)
        {
            var project = await _projectRepo.GetByIdAsync(id);
            if (project is not null)
            {
                _projectRepo.DeleteAsync(project);
                await _projectRepo.SaveChangesAsync();
            }
        }

    }
}
