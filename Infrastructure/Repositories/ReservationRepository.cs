using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _db;

    public ReservationRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Reservation>> GetByDateAsync(DateTime date)
    {
        var d = DateOnly.FromDateTime(date);

        return await _db.Reservations
            .Where(r => r.BookDate == d)
            .ToListAsync();
    }

    public async Task<List<Reservation>> GetByUserAsync(string userId)
    {
        return await _db.Reservations
            .Include(r => r.Seat)
            .ThenInclude(s => s.Room)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.BookDate)
            .ToListAsync();
    }

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        var exists = await _db.Reservations.AnyAsync(r =>
            r.SeatId == reservation.SeatId &&
            r.BookDate == reservation.BookDate);

        if (exists)
            throw new InvalidOperationException("Místo je pro daný den již rezervováno.");

        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync();
        return reservation;
    }
}