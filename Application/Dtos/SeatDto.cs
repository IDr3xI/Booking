namespace Application.Dtos;

public class SeatDto
{
    public int Id { get; set; }
    public string SeatCode { get; set; } = string.Empty;
    public int RoomId { get; set; }

    public string? RoomName { get; set; }
}
