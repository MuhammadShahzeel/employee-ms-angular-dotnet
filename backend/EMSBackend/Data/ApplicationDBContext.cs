using EMSBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EMSBackend.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {


        }
        public DbSet< Employee> Employees { get; set; }

    }
}
