using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=workers.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Lastname).HasMaxLength(100);
                entity.Property(e => e.Role).IsRequired();
                entity.Property(e => e.Salary).IsRequired().HasColumnType("TEXT");
            });

            modelBuilder.Entity<Worker>().HasData(
                new Worker { Id = 1, Name = "John", Surname = "Doe", Role = WorkerRole.Developer, Salary = 3500.00m },
                new Worker { Id = 2, Name = "Jane", Surname = "Smith", Lastname = "A.", Role = WorkerRole.Tester, Salary = 2800.50m },
                new Worker { Id = 3, Name = "Peter", Surname = "Jones", Role = WorkerRole.Manager, Salary = 5000.00m }
            );
        }
    }
}
