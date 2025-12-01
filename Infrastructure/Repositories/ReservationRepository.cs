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
}