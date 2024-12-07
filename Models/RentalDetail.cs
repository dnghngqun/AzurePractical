namespace ComicSystem.Models
{
    public class RentalDetail
    {
        public int RentalDetailID { get; set; }
        public int RentalID { get; set; }
        public int ComicBookID { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }

        // Khóa ngoại đến Rental
        public Rental Rental { get; set; }

        // Khóa ngoại đến ComicBook
        public ComicBook ComicBook { get; set; }
    }
}