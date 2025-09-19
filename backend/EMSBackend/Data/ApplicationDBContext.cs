using EMSBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMSBackend.Data
{
    public class ApplicationDBContext: IdentityDbContext<User>

    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {


        }
        public DbSet< Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
 


    }
}
