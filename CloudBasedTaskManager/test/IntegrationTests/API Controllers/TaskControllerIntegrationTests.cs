using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI.Services.Interfaces;
using Moq;
using Core.DTOs;
using Microsoft.Extensions.DependencyInjection;
using TaskStatus = Core.DTOs.TaskStatus;

namespace IntegrationTests.API_Controllers
{
    public class TaskControllerIntegrationTests : IClassFixture<WebApplicationFactory<WebAPI.Program>>
    {
        private readonly HttpClient _client;
        private readonly Mock<ITaskService> _taskServiceMock;

        public TaskControllerIntegrationTests(WebApplicationFactory<WebAPI.Program> factory)
        {
            _taskServiceMock = new Mock<ITaskService>();

            // Set up the test server
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing ITaskService service descriptor
                    var serviceDescriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(ITaskService));

                    if (serviceDescriptor != null)
                    {
                        services.Remove(serviceDescriptor);
                    }

                    // Add the mock ITaskService
                    services.AddScoped(_ => _taskServiceMock.Object);
                });
            }).CreateClient();
        }

        [Fact]
        public async Task CreateAsync_ValidTask_ReturnsCreatedResponse()
        {
            // Arrange
            var taskDto = new TaskDTO
            {
                Id = 0, // Set ID to 0 for new task (assuming ID will be generated on creation)
                Title = "Test Task Title", // Title of the task
                Description = "This is a test task description.", // Optional description
                DueDate = DateTime.UtcNow.AddDays(1), // Provide a valid due date (1 day in the future)
                Priority = TaskPriority.Medium, // Set a valid priority
                Status = TaskStatus.ToDo, // Optional: Set default status
                AssignedTo = "Test User" // Optional: Set assignee name if needed
            };

            // Setup the service mock to return the taskDto when CreateAsync is called
            _taskServiceMock.Setup(service => service.CreateAsync(It.IsAny<TaskDTO>()))
                .ReturnsAsync(taskDto);

            // Act
            var response = await _client.PostAsJsonAsync("/api/task", taskDto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var createdTask = await response.Content.ReadFromJsonAsync<TaskDTO>();
            Assert.Equal(taskDto.Title, createdTask.Title);
            Assert.Equal(taskDto.Id, createdTask.Id);
        }

        [Fact]
        public async Task CreateAsync_EmptyTask_ReturnsBadRequest()
        {
            // Arrange
            var taskDto = new TaskDTO(); // Create an empty TaskDTO object

            // Act
            var response = await _client.PostAsJsonAsync("/api/task", taskDto); // Pass the empty object

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateAsync_InvalidTask_ReturnsBadRequest()
        {
            // Arrange
            var taskDto = new TaskDTO
            {
                Id = 0,
                Title = null, // Invalid because Title is required
                DueDate = DateTime.UtcNow.AddDays(-1), // Assuming the validation fails because of a past due date
                Priority = TaskPriority.Medium
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/task", taskDto); // Pass the invalid object

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetTaskById_ValidId_ReturnsOkResponse()
        {
            // Arrange
            var taskDto = new TaskDTO { Id = 1, Title = "Test Task" };

            _taskServiceMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync(taskDto);

            // Act
            var response = await _client.GetAsync("/api/task/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var returnedTask = await response.Content.ReadFromJsonAsync<TaskDTO>();
            Assert.Equal(taskDto.Title, returnedTask.Title);
            Assert.Equal(taskDto.Id, returnedTask.Id);
        }

        [Fact]
        public async Task GetTaskById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            _taskServiceMock.Setup(service => service.GetByIdAsync(1))
                .ThrowsAsync(new KeyNotFoundException("Task not found."));

            // Act
            var response = await _client.GetAsync("/api/task/1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
