using DncerBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DncerBackend.AppContext
{
    public class AppDBContext : DbContext
    {
        public DbSet<Cars> Cars { get; set; }
        public DbSet<CarParts> CarParts { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DincerDB;Trusted_Connection=True;");
        //}
    }
}
