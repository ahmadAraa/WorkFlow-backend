using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


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
        [Authorize]
        public IActionResult AddProject([FromBody] ProjectVM projectVM)
        {
            var userIdClaim = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("Invalid token: missing user ID.");

            var userId = int.Parse(userIdClaim);

            // pass both the project info and user id to your service
            var project = _projectService.AddProject(projectVM, userId);

            return Ok(project);
        }

        [HttpGet("get-all-projects")]
        public async Task<IActionResult> GetAllProjects()
        {
           var project = await _projectService.GetAllProjects();
            return Ok(project);
        }
        [HttpGet("get-project/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            await _projectService.GetProjectById(id);
            return Ok();
        }
        [HttpPut("edit-project/{id}")]
        public async Task<IActionResult> EditProjectById(int id,ProjectVM projectVM) { 
          await _projectService.EditProject(id,projectVM);
            return Ok();
        }
        [HttpDelete("remove-project/{id}")]
        public async Task<IActionResult> DeleteProjectById(int id) { 
      await  _projectService.DeleteProject(id);
            return Ok();    
        }

    }
}
