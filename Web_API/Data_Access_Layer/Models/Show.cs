using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Show
    {
        [Key]
        public int ShowId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Timing { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal Price { get; set; }
        public int ScreenNumber { get; set; }

        // Foreign Key to Movie (One-to-Many)
        public int MovieId { get; set; }

        [JsonIgnore]
        public Movie? Movie { get; set; }

        // Many-to-Many Relationship with User through Ticket
        [JsonIgnore]
        public ICollection<Ticket>? Tickets { get; set; }
    }




}
