using System.ComponentModel.DataAnnotations;
using Core.DTOs;
using Infrastructure.Interfaces;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDTO> CreateAsync(TaskDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Task cannot be null.");
            }

            // Validate the DTO
            ValidateTask(dto);

            dto.Id = 0; // Ensure that the ID is not set for new tasks

            return await _taskRepository.CreateAsync(dto);
        }

        public async Task<TaskDTO> DeleteAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }

            await _taskRepository.DeleteAsync(id);
            return task;
        }

        public async Task<List<TaskDTO>> GetAllAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<TaskDTO> GetByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }

            return task;
        }

        public async Task<TaskDTO> UpdateAsync(TaskDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Task cannot be null.");
            }

            // Validate the DTO
            ValidateTask(dto);

            var existingTask = await _taskRepository.GetByIdAsync(dto.Id);
            if (existingTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {dto.Id} not found.");
            }

            return await _taskRepository.UpdateAsync(dto);
        }

        private void ValidateTask(TaskDTO task)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(task);

            if (!Validator.TryValidateObject(task, validationContext, validationResults, true))
            {
                // If validation fails, throw a validation exception with all error messages
                throw new ValidationException("Task validation failed: " + string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
            }
        }
    }
}
