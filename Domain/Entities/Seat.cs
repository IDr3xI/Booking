using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Seat
{
    [Key]
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
    public string Code { get; set; } = string.Empty;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [NotMapped]
    public int DaysReserved { get; set; }
}
