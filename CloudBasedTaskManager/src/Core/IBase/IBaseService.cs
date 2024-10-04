namespace Core.IBase
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAllAsync();               // Get all items of type T
        Task<T> GetByIdAsync(int id);              // Get item by ID of type T
        Task<T> CreateAsync(T dto);                  // Create a new item of type T
        Task<T> UpdateAsync(T dto);                  // Update an existing item of type T
        Task<T> DeleteAsync(int id);                   // Delete an item by ID
    }
}
