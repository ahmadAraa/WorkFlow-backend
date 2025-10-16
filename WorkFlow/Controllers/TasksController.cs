using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using Services;
using System;
using Microsoft.Extensions.Logging; 
using System.Threading.Tasks;

namespace WorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _tasksService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(TaskService tasksService, ILogger<TasksController> logger)
        {
            this._tasksService = tasksService;
            _logger = logger;
        }

        [HttpPost("add-task")]
        public IActionResult AddTask([FromBody] ActivityVM tasksVM)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Attempting to add new task: {TaskName}", tasksVM.Name);
            }

            try
            {
                _tasksService.AddTask(tasksVM);

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Task {TaskName} added successfully.", tasksVM.Name);
                }
            }
            catch (Exception ex)
            {
                // Critical failure logging with the full exception (ex)
                _logger.LogError(ex, "Critical failure adding task: {TaskName}.", tasksVM.Name);
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("remove-tasks-by-project/{projectId}")]
        public async Task<IActionResult> RemoveProjectTask(int?projectId) { 
        var removedTasks =await _tasksService.DeleteAllTaskProject(projectId);
            return Ok(removeTask);
        }

        [HttpDelete("remove-task/{id}")]
        public IActionResult removeTask(int? id)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Attempting to remove task with ID: {TaskId}", id);
            }

            try
            {
                _tasksService.RemoveTask(id);

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Task with ID {TaskId} removed successfully.", id);
                }
            }
            catch (Exception ex)
            {
                // Critical failure logging with the full exception (ex)
                _logger.LogError(ex, "Critical failure removing task with ID: {TaskId}.", id);
                return BadRequest("Failed to remove task due to an error.");
            }

            return Ok();
        }

        [HttpGet("get-task/{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Attempting to retrieve task with ID: {TaskId}", id);
            }

            try
            {
                var task = await _tasksService.GetTaskById(id);

                if (task == null)
                {
                    // Log as a Warning since the code didn't break, but the resource wasn't found
                    _logger.LogWarning("Task with ID {TaskId} not found.", id);
                    return NotFound($"Task with ID {id} not found.");
                }

                _logger.LogInformation("Task with ID {TaskId} retrieved successfully.", id);
                return Ok(task);
            }
            catch (Exception ex)
            {
                // Critical failure logging
                _logger.LogError(ex, "Critical failure retrieving task with ID: {TaskId}.", id);
                // Return 500 for unexpected internal errors
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("get-all-tasks")]
        public async Task<IActionResult> GetAllTasks([FromQuery]int? projectId)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Attempting to retrieve all tasks.");
            }

            try
            {
                var tasks = await _tasksService.GetAllTasks(projectId);
                _logger.LogInformation("{Count} tasks retrieved successfully.", tasks?.Count ?? 0);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                // Critical failure logging
                _logger.LogError(ex, "Critical failure retrieving all tasks.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("edit-task/{id}")]
        public async Task<IActionResult> EditTasks(int? id, ActivityVM taskVM)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Attempting to edit task with ID: {TaskId}. New name: {TaskName}", id, taskVM?.Name);
            }

            try
            {
                var newTask = await _tasksService.EditTask(id, taskVM);

                if (newTask == null)
                {
                    _logger.LogWarning("Edit failed: Task with ID {TaskId} not found.", id);
                    return NotFound($"Task with ID {id} not found.");
                }

                _logger.LogInformation("Task with ID {TaskId} edited successfully.", id);
                return Ok(newTask);
            }
            catch (Exception ex)
            {
                // Critical failure logging
                _logger.LogError(ex, "Critical failure editing task with ID: {TaskId}.", id);
                return BadRequest("Failed to edit task due to an error.");
            }
        }

        [HttpDelete("remove-all-tasks")]
        public async Task<IActionResult> DeleteAllTasks()
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Attempting to delete all tasks.");
            }

            try
            {
                var rowsAffected = await _tasksService.RemoveAllTasks();

                if (rowsAffected > 0)
                {
                    _logger.LogInformation("{RowsAffected} tasks deleted successfully.", rowsAffected);
                    return Ok(new { message = $"successfully removed {rowsAffected} tasks." });
                }

                _logger.LogInformation("No tasks were deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                // Critical failure logging
                _logger.LogError(ex, "Critical failure deleting all tasks.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred while deleting all tasks.");
            }
        }
    }
}