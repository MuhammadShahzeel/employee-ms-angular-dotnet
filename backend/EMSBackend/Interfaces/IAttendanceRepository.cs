using EMSBackend.Helpers;
using EMSBackend.Models;

namespace EMSBackend.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendence>
    {
        Task<PagedResult<Attendence>> GetAttendanceHistoryAsync(SearchOptions options);
    }
}
