using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int SeatId { get; set; }
        public Seat? Seat { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateOnly BookDate { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
