using Domain.Entities;

namespace Application.Services;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task CreateAsync(Reservation reservation);
    Task DeleteAsync(int id);
}