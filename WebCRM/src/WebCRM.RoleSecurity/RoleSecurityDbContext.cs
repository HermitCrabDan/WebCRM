namespace WebCRM.RoleSecurity
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class RoleSecurityDbContext: DbContext
    {
        public RoleSecurityDbContext(DbContextOptions<RoleSecurityDbContext> options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(k => k.Id);

            modelBuilder.Entity<User>().HasIndex(k => new { k.Username, k.Password });

            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}