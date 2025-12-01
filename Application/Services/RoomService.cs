using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repo;

    public RoomService(IRoomRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Room>> GetRoomsWithSeatsAsync()
    {
        return _repo.GetAllWithSeatsAsync();
    }
}
