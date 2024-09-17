using Microsoft.EntityFrameworkCore;
using Repository.Models.Domain;

namespace Repository.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
