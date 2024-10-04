using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateAsync([FromBody] TaskDTO task)
        {
            try
            {
                var createdTask = await _taskService.CreateAsync(task);
                return Created(string.Empty, createdTask);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTaskById(int id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id); 

                return Ok(task); 
            }
            catch (KeyNotFoundException)
            {
                // Return NotFound for specific exception
                return NotFound();
            }
            catch (Exception e)
            {
                // Log the exception (optional)
                Console.WriteLine(e);
                // Return Internal Server Error for other unexpected exceptions
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
