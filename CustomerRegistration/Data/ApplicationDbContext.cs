using CustomerRegistration.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerRegistration.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
