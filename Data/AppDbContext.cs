using Microsoft.EntityFrameworkCore;
using RustedMods.Models;

namespace RustedMods.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Addon database
        public DbSet<Addon> Addons { get; set; }
        public DbSet<UserAccount> Users { get; set; }
    }
}
