using EMSBackend.Helpers;
using EMSBackend.Models;

namespace EMSBackend.Interfaces
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        // Repository-level method jo search + paging handle karega
        Task<PagedResult<Department>> GetAllAsync(SearchOptions options);
    }
   
}
