using EMSBackend.Helpers;
using EMSBackend.Models;

namespace EMSBackend.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Repository-level method jo search + paging handle karega
        Task<List<Employee>> GetAllAsync(SearchOptions options);
    }
}
