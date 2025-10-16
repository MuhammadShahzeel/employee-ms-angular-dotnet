using EMSBackend.Data;
using EMSBackend.Helpers;
using EMSBackend.Interfaces;
using EMSBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EMSBackend.Repositories
{
    public class AttendanceRepository : Repository<Attendence>, IAttendanceRepository
    {
        private readonly ApplicationDBContext _context;

        public AttendanceRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResult<Attendence>> GetAttendanceHistoryAsync(SearchOptions options)
        {
            var query = _context.Attendences.AsNoTracking().AsQueryable();

            // Filter by employee
            if (options.EmployeeId.HasValue)
                query = query.Where(x => x.EmployeeId == options.EmployeeId.Value);

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination if given
            if (options.PageIndex.HasValue)
            {
                query = query
                    .Skip(options.PageIndex.Value * options.PageSize)
                    .Take(options.PageSize);
            }

            var data = await query.ToListAsync();

            return new PagedResult<Attendence>
            {
                TotalCount = totalCount,
                Data = data
            };
        }
    }
}
