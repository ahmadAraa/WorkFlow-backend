using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Services;
using System.Threading.Tasks;

namespace WorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _tasksService;
        public TasksController(TaskService tasksService)
        {
            this._tasksService = tasksService;
        }
        [HttpPost("add-task")]
        public IActionResult AddTask([FromBody]TasksVM tasksVM)
        {
            _tasksService.AddTask(tasksVM);
            return Ok();
        }
        [HttpDelete("remove-task/{id}")]
        public IActionResult removeTask( int? id) {
            _tasksService.RemoveTask(id);
            return Ok();
        }
        [HttpGet("get-task/{id}")]
        public async Task<IActionResult> GetTask(int id) {
        var task = await _tasksService.GetTaskById(id); 
        return Ok(task);
        }
        [HttpGet("get-all-tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _tasksService.GetAllTasks();
            return Ok(tasks);
        }
        [HttpPut("edit-task/{id}")]
        public async Task<IActionResult> EditTasks(int ?id,TasksVM taskVM) {
           var newTask =  await _tasksService.EditTask(id,taskVM);
            return Ok(newTask);
        }
        [HttpDelete("remove-all-tasks")]
        public async Task<IActionResult> DeleteAllTasks()
        {
          var rowsAffected = await _tasksService.RemoveAllTasks();
            if (rowsAffected > 0)
            {

                return Ok(new {message = $"successfully removed {rowsAffected} tasks." });
            }
            return NoContent();
        }


    }
}
