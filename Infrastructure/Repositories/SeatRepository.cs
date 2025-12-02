using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly AppDbContext _db;

    public SeatRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Seat>> GetAllAsync()
    {
        return await _db.Seats
            .Include(s => s.Room)
            .ToListAsync();
    }

    public async Task<List<Seat>> GetByRoomIdAsync(int roomId)
    {
        return await _db.Seats
            .Where(s => s.RoomId == roomId)
            .Include(s => s.Room)
            .ToListAsync();
    }

    public async Task<Seat?> GetByIdAsync(int id)
    {
        return await _db.Seats
            .Include(s => s.Room)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Seat seat)
    {
        _db.Seats.Add(seat);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Seat seat)
    {
        _db.Seats.Update(seat);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Seat seat)
    {
        _db.Seats.Remove(seat);
        await _db.SaveChangesAsync();
    }
}
