using Domain.Entities;

namespace Application.Interfaces;

public interface IReservationRepository
{
    Task<List<Reservation>> GetByDateAsync(DateTime date);
    Task<List<Reservation>> GetByUserAsync(string userId);
    Task<Reservation> CreateAsync(Reservation reservation);
    Task<bool> DeleteAsync(int reservationId);
    Task<List<Seat>> GetSeatUtilizationAsync(DateTime from, DateTime to);
}