using EaseTrail.WebApp.Models;
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
        public DbSet<WorkSpace> WorkSpaces { get; set; }
        public DbSet<UsersWorkSpace> UsersWorkSpaces { get; set; }

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

            #region User

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

            #endregion

            #region WorkSpace

            modelBuilder.Entity<WorkSpace>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<WorkSpace>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.WorkSpaces)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkSpace>()
                .Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<WorkSpace>()
                .Property(u => u.Description)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<WorkSpace>()
                .Property(u => u.Color)
                .HasMaxLength(25)
                .IsRequired();

            modelBuilder.Entity<WorkSpace>()
                .Property(u => u.Status)
                .IsRequired();

            #endregion

            #region UserWorkSpace

            modelBuilder.Entity<UsersWorkSpace>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<UsersWorkSpace>()
                .HasOne(x => x.WorkSpace)
                .WithMany(x => x.UsersWorkSpaces)
                .HasForeignKey(x => x.WorkSpaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsersWorkSpace>()
                .Property(u => u.ColaboratorType)
                .IsRequired();

            modelBuilder.Entity<UsersWorkSpace>()
                .Property(u => u.InviteStatus)
                .IsRequired();

            modelBuilder.Entity<UsersWorkSpace>()
                .Property(u => u.UserEmail)
                .HasMaxLength(50)
                .IsRequired();

            #endregion
        }
    }
}
