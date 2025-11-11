using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.Seat!)
                .ThenInclude(s => s.Room!)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.Seat!)
                .ThenInclude(s => s.Room!)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task CreateAsync(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var r = await _context.Reservations.FindAsync(id);
        if (r != null)
        {
            _context.Reservations.Remove(r);
            await _context.SaveChangesAsync();
        }
    }
}

