using Microsoft.EntityFrameworkCore;


namespace PMTK_TestWork.Details
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public static string ConnString { get; set; } = "Host=localhost;Port=5432;Database=PMTK;Username=postgres;Password=2311";

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasColumnName("name");

            modelBuilder.Entity<User>()
                .Property(u => u.DateOfBirth)
                .HasColumnName("dateofbirth");

            modelBuilder.Entity<User>()
                .Property(u => u.Gender)
                .HasColumnName("gender");
        }
    }
}
