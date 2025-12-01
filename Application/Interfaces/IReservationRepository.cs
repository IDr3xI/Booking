using Domain.Entities;

namespace Application.Interfaces;

public interface IReservationRepository
{
    Task<List<Reservation>> GetByDateAsync(DateTime date);
}