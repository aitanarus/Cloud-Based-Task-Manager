using Core.DTOs;
using System.ComponentModel.DataAnnotations;
using TaskStatus = Core.DTOs.TaskStatus;

namespace UnitTests.DTOs
{
    public class TaskDTOTests
    {
        [Fact]
        public void TaskDTO_RequiredFields_ShouldHaveValidationErrors_WhenMissingRequiredFields()
        {
            // Arrange
            var task = new TaskDTO
            {
                Title = "", // Invalid
                DueDate = null, // Use null for invalid
                Priority = TaskPriority.Low,
                Status = TaskStatus.ToDo,
                AssignedTo = "John Doe"
            };

            // Act
            var validationResults = ValidateModel(task);

            // Assert
            Assert.Contains(validationResults, v => v.ErrorMessage == "Task title is required.");
            Assert.Contains(validationResults, v => v.ErrorMessage == "Due date is required.");
        }


        [Fact]
        public void TaskDTO_Title_TooShortOrTooLong_ShouldHaveValidationErrors()
        {
            // Arrange
            var task = new TaskDTO
            {
                Title = "A", // Too short
                DueDate = DateTime.Now.AddDays(1),
                Priority = TaskPriority.Low,
                Status = TaskStatus.ToDo,
                AssignedTo = "John Doe"
            };

            // Act
            var validationResults = ValidateModel(task);

            // Assert
            Assert.Contains(validationResults, v => v.ErrorMessage == "Task title must be between 3 and 100 characters.");

            // Modify title to be too long
            task.Title = new string('A', 101); // 101 characters

            // Act again
            validationResults = ValidateModel(task);

            // Assert
            Assert.Contains(validationResults, v => v.ErrorMessage == "Task title must be between 3 and 100 characters.");
        }

        [Fact]
        public void TaskDTO_Description_TooLong_ShouldHaveValidationError()
        {
            // Arrange
            var task = new TaskDTO
            {
                Title = "Valid Task",
                Description = new string('A', 501), // 501 characters, too long
                DueDate = DateTime.Now.AddDays(1),
                Priority = TaskPriority.Medium,
                Status = TaskStatus.InProgress
            };

            // Act
            var validationResults = ValidateModel(task);

            // Assert
            Assert.Contains(validationResults, v => v.ErrorMessage == "Description cannot exceed 500 characters.");
        }

        [Fact]
        public void TaskDTO_AssignedTo_TooLong_ShouldHaveValidationError()
        {
            // Arrange
            var task = new TaskDTO
            {
                Title = "Valid Task",
                DueDate = DateTime.Now.AddDays(1),
                Priority = TaskPriority.High,
                Status = TaskStatus.Done,
                AssignedTo = new string('A', 51) // 51 characters, too long
            };

            // Act
            var validationResults = ValidateModel(task);

            // Assert
            Assert.Contains(validationResults, v => v.ErrorMessage == "Assignee name cannot exceed 50 characters.");
        }

        [Fact]
        public void TaskDTO_ParameterizedConstructor_ShouldInitializeCorrectly()
        {
            // Arrange
            var dueDate = DateTime.Now.AddDays(3);
            var task = new TaskDTO(1, "Test Task", "This is a test task.", dueDate, TaskPriority.Medium, "John Smith", TaskStatus.ToDo);

            // Assert
            Assert.Equal(1, task.Id);
            Assert.Equal("Test Task", task.Title);
            Assert.Equal("This is a test task.", task.Description);
            Assert.Equal(dueDate, task.DueDate);
            Assert.Equal(TaskPriority.Medium, task.Priority);
            Assert.Equal("John Smith", task.AssignedTo);
            Assert.Equal(TaskStatus.ToDo, task.Status); // Default
        }

        [Fact]
        public void TaskDTO_DefaultValues_ShouldBeSetCorrectly()
        {
            // Arrange
            var task = new TaskDTO();

            // Assert
            Assert.Equal(TaskStatus.ToDo, task.Status); // Default status
        }

        // Helper method to validate data annotations
        private IList<ValidationResult> ValidateModel(object model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
    }
}
