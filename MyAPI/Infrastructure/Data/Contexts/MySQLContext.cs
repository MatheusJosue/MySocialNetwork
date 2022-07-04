using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAPI.Domain.Models;

namespace MyAPI.Infrastructure.Data.Contexts
{
    public class MySQLContext : IdentityDbContext<ApplicationUser>
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }
        public DbSet<Post> Post { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<ApplicationRole> Role { get; set; }
        public DbSet<Friend> Friend { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //aplicando ApplicationUser a tabela AspNetUsers
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            modelBuilder.Entity<Post>();
            modelBuilder.Entity<Like>();
            modelBuilder.Entity<Message>();
            modelBuilder.Entity<Friend>();
        }
    }
}
