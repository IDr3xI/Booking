using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seed;

public static class DatabaseSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, Name = "RED Zone" },
            new Room { Id = 2, Name = "GREEN Zone" },
            new Room { Id = 3, Name = "BLUE Zone" }
        );
        modelBuilder.Entity<Seat>().HasData(
            new Seat { Id = 1, RoomId = 1, Code = "R1" },
            new Seat { Id = 2, RoomId = 1, Code = "R2" },
            new Seat { Id = 3, RoomId = 2, Code = "G1" },
            new Seat { Id = 4, RoomId = 2, Code = "G2" },
            new Seat { Id = 5, RoomId = 3, Code = "B1" },
            new Seat { Id = 6, RoomId = 3, Code = "B2" }
        );
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "johndoe", Email = "john.doe@seyfor.com" }
        );
    }
}