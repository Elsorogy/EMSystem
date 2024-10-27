using EmployeeManagmentSystem.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagmentSystem.Data
{
    public class EMSContext : IdentityDbContext<ApplicationUser>
    {
        public EMSContext(DbContextOptions options):base(options) { }
        
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
