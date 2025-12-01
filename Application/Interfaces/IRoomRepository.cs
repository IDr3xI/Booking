using Domain.Entities;

namespace Application.Interfaces;

public interface IRoomRepository
{
    Task<List<Room>> GetAllWithSeatsAsync();
}

