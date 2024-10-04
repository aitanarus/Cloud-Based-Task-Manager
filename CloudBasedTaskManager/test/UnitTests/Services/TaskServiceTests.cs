using Core.DTOs;
using Infrastructure.Interfaces;
using Moq;
using WebAPI.Services;


namespace UnitTests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _taskService = new TaskService(_taskRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidTask_ReturnsTask()
        {
            // Arrange
            var taskDto = new TaskDTO { Title = "New Task", DueDate = DateTime.Now.AddDays(1), Priority = TaskPriority.Low };
            _taskRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<TaskDTO>()))
                .ReturnsAsync(taskDto);

            // Act
            var result = await _taskService.CreateAsync(taskDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Task", result.Title);
        }

        [Fact]
        public async Task UpdateAsync_ValidTask_ReturnsUpdatedTask()
        {
            // Arrange
            var taskDto = new TaskDTO { Id = 1, Title = "Updated Task" };
            _taskRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<TaskDTO>()))
                .ReturnsAsync(taskDto);
            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(taskDto); // Mock GetByIdAsync to return the task being updated

            // Act
            var result = await _taskService.UpdateAsync(taskDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Task", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsTask()
        {
            // Arrange
            var taskDto = new TaskDTO { Id = 1, Title = "Task to Get" };
            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(taskDto);

            // Act
            var result = await _taskService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Task to Get", result.Title);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsDeleteOnce()
        {
            // Arrange
            int taskId = 1;
            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(taskId)) // Mock GetByIdAsync to prevent KeyNotFoundException
                .ReturnsAsync(new TaskDTO { Id = taskId, Title = "Task to Delete" });

            // Act
            await _taskService.DeleteAsync(taskId);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.DeleteAsync(taskId), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_NullTask_ThrowsArgumentNullException()
        {
            // Arrange
            TaskDTO nullTask = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _taskService.CreateAsync(nullTask));
        }

        // Additional test cases can be added similarly...
    }
}
