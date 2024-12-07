namespace ComicSystem.Models
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int CustomerID { get; set; }
        public String Status { get; set; } = "Renting"; //Returned,Overdued,
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }

        // Khóa ngoại đến Customer
        public Customer Customer { get; set; }

        // Quan hệ 1-N với RentalDetails
        public ICollection<RentalDetail> RentalDetails { get; set; }
    }
}