using Domain.Entities;

namespace Application.Interfaces;

public interface IReservationRepository
{
    Task<List<Reservation>> GetByDateAsync(DateTime date);
    Task<List<Reservation>> GetByUserAsync(string userId);
    Task<Reservation> CreateAsync(Reservation reservation);
}