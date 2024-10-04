using Core.DTOs;
using Core.IBase;

namespace Infrastructure.Interfaces
{
    public interface ITaskRepository : IBaseRepository<TaskDTO>
    {
        //Additional Methods Can be specified
    }
}
