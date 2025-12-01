using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repo;

    public ReservationService(IReservationRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Reservation>> GetReservationsByDateAsync(DateTime date)
    {
        return _repo.GetByDateAsync(date);
    }
}