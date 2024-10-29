using Microsoft.AspNetCore.Identity;

namespace Data_Access_Layer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
