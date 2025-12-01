using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Room
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
