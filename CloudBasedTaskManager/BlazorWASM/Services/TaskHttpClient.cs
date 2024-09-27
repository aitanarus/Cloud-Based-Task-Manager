using BlazorWASM.HttpClients.Interfaces;
using Shared.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorWASM.HttpClients
{
    public class TaskHttpClient : ITaskService
    {
        private readonly HttpClient client;

        public TaskHttpClient(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("TaskService");
        }

        public async Task<TaskDTO> CreateAsync(TaskDTO dto)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("", dto);
            string result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }

            TaskDTO task = JsonSerializer.Deserialize<TaskDTO>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

            return task;
        }

        public async Task<TaskDTO> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TaskDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskDTO> UpdateAsync(TaskDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
