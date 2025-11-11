using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Interfaces;

public interface ISeatService
{
    Task<IEnumerable<SeatDto>> GetByRoomAsync(int roomId);
}
