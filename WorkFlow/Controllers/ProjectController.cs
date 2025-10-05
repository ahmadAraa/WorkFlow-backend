using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Services;

namespace WorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ILogger<ProjectController> _logger;   
        public ProjectController(ProjectService projectService, ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _logger = logger;
            
        }
        [HttpPost("add-project")]
        public IActionResult AddProject([FromBody]ProjectVM projectVM)
        {
            var project =  _projectService.AddProject(projectVM);
            return Ok(project);
        }
        [HttpGet("get-all-projects")]
        public async Task<IActionResult> GetAllProjects()
        {
           var project = await _projectService.GetAllProjects();
            return Ok(project);
        }
        [HttpGet("get-project/{id}")]
        public IActionResult GetProjectById(int?id)
        {
            _projectService.GetProjectById(id);
            return Ok();
        }
        [HttpPut("edit-project/{id}")]
        public IActionResult EditProjectById(int?id,ProjectVM projectVM) { 
            _projectService.EditProject(id,projectVM);
            return Ok();
        }
        [HttpDelete("remove-project/{id}")]
        public IActionResult DeleteProjectById(int? id) { 
        _projectService.DeleteProject(id);
            return Ok();    
        }

    }
}
