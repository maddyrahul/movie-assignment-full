
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public string? Director { get; set; }
        public string? Description { get; set; }

        // One-to-Many Relationship with Show
        [JsonIgnore]
        public ICollection<Show> Shows { get; set; }
    }



}
