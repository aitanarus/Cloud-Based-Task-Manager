using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class TaskDTO
    {
        // Unique identifier for the task
        public int Id { get; set; }

        // Title of the task
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        // Detailed description of the task
        [StringLength(500)]
        public string Description { get; set; }

        // Due date for the task to be completed
        [Required]
        public DateTime DueDate { get; set; }

        // Priority level of the task
        [Required]
        public TaskPriority Priority { get; set; }

        // Status of the task (To Do, In Progress, Done)
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;

        // Optional: Assignee of the task (user ID or name)
        public string AssignedTo { get; set; }

        // Parameterized constructor
        public TaskDTO(int id, string title, string description, DateTime dueDate, TaskPriority priority, string assignedTo = null)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            AssignedTo = assignedTo;
            Status = TaskStatus.ToDo; // Default status is ToDo
        }
    }

    // Enum for task priority levels
    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    // Enum for task status
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}
