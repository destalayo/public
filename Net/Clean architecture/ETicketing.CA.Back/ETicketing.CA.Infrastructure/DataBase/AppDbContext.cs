
using ETicketing.CA.Domain.Models.Reservations;
using ETicketing.CA.Domain.Models.Rooms;
using ETicketing.CA.Domain.Models.Seasons;
using ETicketing.CA.Domain.Models.Seats;
using ETicketing.CA.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace ETicketing.CA.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Seat> Seats { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)       {        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(x => x.Seats).WithOne(x => x.Room).HasForeignKey(x => x.RoomId);
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<Season>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Room).WithMany(x => x.Seasons).HasForeignKey(x => x.RoomId);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(x => new { x.SeasonId, x.SeatId });
                entity.HasOne(x => x.Season).WithMany(x => x.Reservations).HasForeignKey(x => x.SeasonId);
                entity.HasOne(x => x.Seat).WithMany(x => x.Reservations).HasForeignKey(x => x.SeatId);
                entity.HasOne(x => x.User).WithMany(x => x.Reservations).HasForeignKey(x => x.UserId);
            });
        }
    }
}
