using System;
using System.Linq;
using Application.Interfaces;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repository;

    public ReservationService(IReservationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(r => new ReservationDto
        {
            Id = r.Id,
            Date = r.BookDate.ToDateTime(TimeOnly.MinValue),
            CreatedAt = r.CreateDate,
            UserDisplayName = r.User?.Username,
            SeatCode = r.Seat?.Code,
            RoomName = r.Seat?.Room?.Name
        }).ToList();
    }

    public async Task<ReservationDto?> GetByIdAsync(int id)
    {
        var r = await _repository.GetByIdAsync(id);
        if (r == null) return null;

        return new ReservationDto
        {
            Id = r.Id,
            Date = r.BookDate.ToDateTime(TimeOnly.MinValue),
            CreatedAt = r.CreateDate,
            UserDisplayName = r.User?.Username,
            SeatCode = r.Seat?.Code,
            RoomName = r.Seat?.Room?.Name
        };
    }

    public async Task CreateAsync(ReservationDto reservation)
    {
        var entity = new Reservation
        {
            BookDate = DateOnly.FromDateTime(reservation.Date),
            CreateDate = reservation.CreatedAt,
            UserId = reservation.UserId,
            SeatId = reservation.SeatId
        };

        await _repository.CreateAsync(entity);
    }

    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);

    Task<IEnumerable<Reservation>> IReservationService.GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    Task<Reservation?> IReservationService.GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task CreateAsync(Reservation reservation)
    {
        return _repository.CreateAsync(reservation);
    }
}
