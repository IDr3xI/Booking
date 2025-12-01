using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Seat
{
    [Key]
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    public string Code { get; set; } = string.Empty;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
