using Core.Enum;
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
        public DateTime? DueDate { get; set; }  // Nullable DateTime for optional due date

        // Indicates if the task is completed
        public TaskState State { get; set; }

        // Review or feedback for the task, includes MoodDTO
        public ReviewDTO Review { get; set; }

        // List of resources related to the task like links, maps directions, etc.
        public List<ResourceDTO> Resources { get; set; }

        // Parameterless constructor for EF or serialization
        public TaskDTO()
        {
            Resources = new List<ResourceDTO>();
            Review = new ReviewDTO(); 
        }

        // Parameterized constructor for convenience
        public TaskDTO(int id, string title, string description, DateTime? dueDate, TaskState state, ReviewDTO review, List<ResourceDTO> resources)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            State = state;
            Review = review;
            Resources = resources;
        }
    }
}
