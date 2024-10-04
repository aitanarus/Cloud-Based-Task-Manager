using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class TaskDTO
    {
        // Unique identifier for the task
        public int Id { get; set; }

        // Title of the task
        [Required(ErrorMessage = "Task title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Task title must be between 3 and 100 characters.")]
        public string Title { get; set; }

        // Detailed description of the task
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        // Due date for the task to be completed
        [Required(ErrorMessage = "Due date is required.")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }  // Change to nullable DateTime

        // Priority level of the task
        [Required(ErrorMessage = "Priority is required.")]
        public TaskPriority Priority { get; set; }

        // Status of the task (To Do, In Progress, Done)
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;

        // Optional: Assignee of the task (user ID or name)
        [StringLength(50, ErrorMessage = "Assignee name cannot exceed 50 characters.")]
        public string AssignedTo { get; set; }

        // Parameterless constructor for EF
        public TaskDTO() { }

        // Parameterized constructor
        public TaskDTO(int id, string title, string description, DateTime? dueDate, TaskPriority priority, string assignedTo, TaskStatus status)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            AssignedTo = assignedTo;
            Status = status;
        }
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}
