using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _db;

    public RoomRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Room>> GetAllWithSeatsAsync()
    {
        return await _db.Rooms
            .Include(r => r.Seats)
            .AsNoTracking()
            .OrderBy(r => r.Id)
            .ToListAsync();
    }
}
