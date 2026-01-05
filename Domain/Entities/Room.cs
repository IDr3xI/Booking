using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Room
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();

    [NotMapped]
    public int DaysReserved { get; set; } = 0;
}
