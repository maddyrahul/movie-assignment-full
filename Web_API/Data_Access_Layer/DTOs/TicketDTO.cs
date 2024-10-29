using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTOs
{
    public class TicketDTO
    {
        public int TicketId { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime BookingDate { get; set; }
        public string? UserId { get; set; }
        public int ShowId { get; set; }
        public string? MovieName { get; set; }
        public int ShowTime { get; set; }
        public int ScreenNumber { get; set; }
    }


}
