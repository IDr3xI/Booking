using Application.Dtos;

namespace Application.Interfaces;

public interface ISeatService
{
    Task<IEnumerable<SeatDto>> GetByRoomAsync(int roomId);
}
