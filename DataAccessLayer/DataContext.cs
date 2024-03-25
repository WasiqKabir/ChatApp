using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Chats> Chats { get; set; }
    }
}
