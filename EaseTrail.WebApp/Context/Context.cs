using EaseTrail.WebApp.Model;
using Microsoft.EntityFrameworkCore;

namespace EaseTrail.WebApp.Services
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;

        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("MeuBancoDeDados");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.DocumentId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Status)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .IsRequired();
        }
    }
}
