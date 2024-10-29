using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data_Access_Layer.DTOs
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public string? Director { get; set; }
        public string? Description { get; set; }

        
    }


}
