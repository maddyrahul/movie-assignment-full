namespace Data_Access_Layer.DTOs
{
    public class ShowDTO
    {
        public int ShowId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Timing { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal Price { get; set; }
        public int ScreenNumber { get; set; }
        public int MovieId { get; set; }
        public string? MovieName { get; set; }
    }


}
