using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<UsersModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UsersModel>().ToTable("tblUsers");
            modelBuilder.Entity<UsersModel>().HasKey(u => u.pkiUserID);
        }
    }
}
