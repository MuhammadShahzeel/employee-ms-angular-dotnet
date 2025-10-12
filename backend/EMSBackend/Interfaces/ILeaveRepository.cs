using EMSBackend.Helpers;
using EMSBackend.Models;

namespace EMSBackend.Interfaces
{
    public interface ILeaveRepository : IRepository<Leave>
    {
      


        //  Admin or general paged list
        Task<PagedResult<Leave>> GetAllAsync(SearchOptions options);

        //  Employee-specific paged list
        Task<PagedResult<Leave>> GetByEmployeeIdAsync(int employeeId, SearchOptions options);

    }
}
