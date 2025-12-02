using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Infrastructure.Data.Seed.DatabaseSeeder.Seed(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Room>()
            .HasMany(r => r.Seats)
            .WithOne(s => s.Room)
            .HasForeignKey(s => s.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Seat>().HasMany(s => s.Reservations).WithOne(r => r.Seat);
        modelBuilder.Entity<User>().HasMany(u => u.Reservations).WithOne(r => r.User);
    }
}