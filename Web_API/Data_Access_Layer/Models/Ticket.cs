using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime BookingDate { get; set; }

        // Foreign Key to User, using string (Guid as string)
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }  // Reference to ApplicationUser

        // Foreign Key to Show
        public int ShowId { get; set; }
        public Show? Show { get; set; }
    }
}
