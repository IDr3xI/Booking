using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;

public class ReservationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SeatId { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }

    public string? SeatCode { get; set; }
    public string? RoomName { get; set; }
    public string? UserDisplayName { get; set; }
}
