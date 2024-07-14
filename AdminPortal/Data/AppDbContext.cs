using AdminPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminPortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) :base(options) 
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
