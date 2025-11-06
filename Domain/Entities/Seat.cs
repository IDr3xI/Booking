using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Seat
    {
        [Key]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        public string Code { get; set; } = string.Empty;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
