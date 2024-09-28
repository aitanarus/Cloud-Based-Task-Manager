using Core.DTOs;
using Infrastructure.Interfaces;

namespace Infrastructure
{
    public class EfcTaskRepository : IEfcTaskRepository
    {
        private readonly YourDbContext _context; // Replace with your actual DbContext

        public EfcTaskRepository(YourDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskDTO>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync(); // Assuming Tasks is your DbSet<TaskDTO>
        }

        public async Task<TaskDTO> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskDTO> CreateAsync(TaskDTO dto)
        {
            await _context.Tasks.AddAsync(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<TaskDTO> UpdateAsync(TaskDTO dto)
        {
            _context.Tasks.Update(dto);
            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<TaskDTO> DeleteAsync(int id)
        {
            var task = await GetByIdAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return task;
            }
            return null; // or throw an exception based on your design
        }
    }
}
