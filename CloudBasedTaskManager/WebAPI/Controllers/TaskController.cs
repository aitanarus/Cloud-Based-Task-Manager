using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private static List<TaskDTO> tasks = new List<TaskDTO>();
        private static int nextId = 1;

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskDTO task)
        {
            // Validate the DTO - Move validation to model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newTask = new TaskDTO(
                nextId++, 
                task.Title,
                task.Description,
                task.DueDate,
                task.Priority,
                task.AssignedTo
            );

            // Add the task to the in-memory list (replace with database in real app)
            tasks.Add(task);

            // Return the created task (201 Created response)
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
    }
}
