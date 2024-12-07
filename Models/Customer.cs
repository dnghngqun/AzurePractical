using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public DateTime RegisterDate { get; set; }

        // Quan hệ 1-N với Rentals
        public ICollection<Rental> Rentals { get; set; }
    }
}