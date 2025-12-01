using Domain.Entities;

namespace Application.Interfaces;

public interface ISeatRepository
{
    Task<List<Seat>> GetAllAsync();
    Task<List<Seat>> GetByRoomIdAsync(int roomId);
    Task<Seat?> GetByIdAsync(int id);
    Task AddAsync(Seat seat);
    Task UpdateAsync(Seat seat);
    Task DeleteAsync(Seat seat);
}
