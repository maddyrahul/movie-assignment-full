using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Data_Access_Layer.Models
{
    public class RegisterModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; } = "User"; // Default to User

        [JsonIgnore]
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
