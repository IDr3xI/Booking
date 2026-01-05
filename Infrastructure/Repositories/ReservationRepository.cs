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
            .Include(r => r.Seat)
            .ThenInclude(s => s!.Room)
            .Include(r => r.User)
            .Where(r => r.BookDate == d)
            .ToListAsync();
    }

    public async Task<List<Reservation>> GetByUserAsync(string userId)
    {
        return await _db.Reservations
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreateDate)
            .Include(r => r.Seat)
                .ThenInclude(s => s!.Room)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        if (reservation.BookDate < today)
            throw new InvalidOperationException("Nelze rezervovat zpětně. Vyber dnešní nebo budoucí datum.");

        var exists = await _db.Reservations.AnyAsync(r =>
            r.SeatId == reservation.SeatId &&
            r.BookDate == reservation.BookDate);

        if (exists)
            throw new InvalidOperationException("Místo je pro daný den již rezervováno.");

        _db.Reservations.Add(reservation);
        await _db.SaveChangesAsync();
        return reservation;
    }

    public async Task<bool> DeleteAsync(int reservationId)
    {
        var entity = await _db.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
        if (entity is null) return false;

        var today = DateOnly.FromDateTime(DateTime.Today);
        if (entity.BookDate < today)
            return false;

        _db.Reservations.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<Seat>> GetSeatUtilizationAsync(DateTime from, DateTime to)
    {
        if (from.Date > to.Date)
            throw new ArgumentException("Datum od nesmí být větší než Datum do.");

        var fromD = DateOnly.FromDateTime(from.Date);
        var toD = DateOnly.FromDateTime(to.Date);

        return await _db.Reservations
            .Where(r => r.BookDate >= fromD && r.BookDate <= toD)
            .GroupBy(r => new
            {
                r.SeatId,
                SeatCode = r.Seat!.Code,
                RoomId = r.Seat!.RoomId,
                RoomName = r.Seat!.Room!.Name
            })
            .Select(g => new Seat
            {
                Id = g.Key.SeatId,
                Code = g.Key.SeatCode,
                RoomId = g.Key.RoomId,
                Room = new Room { Id = g.Key.RoomId, Name = g.Key.RoomName },
                DaysReserved = g.Count()
            })
            .OrderByDescending(x => x.DaysReserved)
            .ThenBy(x => x.Room!.Name)
            .ThenBy(x => x.Code)
            .ToListAsync();
    }
}