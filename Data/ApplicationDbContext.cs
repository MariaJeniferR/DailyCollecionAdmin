using Microsoft.EntityFrameworkCore;
using DailyCashCollection.Models;

namespace DailyCashCollection.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Owner> Owner { get; set; }



    }
}
