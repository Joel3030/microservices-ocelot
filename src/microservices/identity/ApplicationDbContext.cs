using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace identity
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                   .Property(e => e.CreatedAt)
                   .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");

            modelBuilder.Entity<User>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'")
                .ValueGeneratedOnUpdate();

        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity)
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
                }

                ((BaseEntity)entity.Entity).UpdatedAt = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

        public DbSet<User> Users => Set<User>();
    }
}