using AuthAPI.Models;
using AuthAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<UsersModel> Users { get; set; }
        public DbSet<vw_ActiveUsers> vw_ActiveUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UsersModel>().ToTable("tblUsers");
            modelBuilder.Entity<UsersModel>().HasKey(u => u.pkiUserID);
            modelBuilder.Entity<vw_ActiveUsers>().HasNoKey();
        }
    }
}
