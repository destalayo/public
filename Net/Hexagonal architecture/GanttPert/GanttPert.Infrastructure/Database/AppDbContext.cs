using GanttPert.Domain.Models.Features;
using GanttPert.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace GanttPert.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<GanttPert.Domain.Models.Tasks.Task> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)       {        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(d => d.Tasks).WithOne(p => p.User)
                .HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(d => d.Tasks).WithOne(p => p.Feature)
                .HasForeignKey(d => d.FeatureId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<GanttPert.Domain.Models.Tasks.Task>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
        }
    }
}
