using Domain.Entities;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<List<Room>> GetRoomsWithSeatsAsync();
}
