using Microsoft.EntityFrameworkCore;

namespace Task1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-5T54JKJS\SQLEXPRESS;Database=testdb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
