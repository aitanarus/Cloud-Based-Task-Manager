namespace Core.IBase
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();               // Get all items of type T
        Task<T> GetByIdAsync(int id);              // Get item by ID of type T
        Task<T> CreateAsync(T entity);                 // Add a new item of type T
        Task<T> UpdateAsync(T entity);              // Update an existing item of type T
        Task<T> DeleteAsync(int id);             // Delete an item by ID
    }
}
