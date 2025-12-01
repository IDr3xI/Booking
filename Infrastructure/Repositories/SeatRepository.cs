using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly AppDbContext _context;

    public SeatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Seat>> GetAllAsync()
    {
        return await _context.Seats
            .Include(s => s.Room)
            .ToListAsync();
    }

    public async Task<List<Seat>> GetByRoomIdAsync(int roomId)
    {
        return await _context.Seats
            .Where(s => s.RoomId == roomId)
            .Include(s => s.Room)
            .ToListAsync();
    }

    public async Task<Seat?> GetByIdAsync(int id)
    {
        return await _context.Seats
            .Include(s => s.Room)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Seat seat)
    {
        _context.Seats.Add(seat);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Seat seat)
    {
        _context.Seats.Update(seat);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Seat seat)
    {
        _context.Seats.Remove(seat);
        await _context.SaveChangesAsync();
    }
}
