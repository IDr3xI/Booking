using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAllAsync();
}
