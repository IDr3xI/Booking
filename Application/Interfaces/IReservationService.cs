using Domain.Entities;

namespace Application.Interfaces;

public interface IReservationService
{
    Task<List<Reservation>> GetReservationsByDateAsync(DateTime date);
}
