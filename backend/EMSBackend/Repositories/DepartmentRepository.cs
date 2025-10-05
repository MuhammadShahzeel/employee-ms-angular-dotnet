using EMSBackend.Data;
using EMSBackend.Helpers;
using EMSBackend.Interfaces;
using EMSBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EMSBackend.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDBContext _context;

        public DepartmentRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResult<Department>> GetAllAsync(SearchOptions options)
        {
            var query = _context.Departments.AsQueryable();

        

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            if (options.PageIndex.HasValue)
            {
                query = query
                    .Skip(options.PageIndex.Value * options.PageSize)
                    .Take(options.PageSize);
            }

            var data = await query.ToListAsync();

            return new PagedResult<Department>
            {
                TotalCount = totalCount,
                Data = data
            };
        }
    }
}
