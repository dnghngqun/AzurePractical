using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class ComicBook
    {

        public int ComicBookID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }

        // Quan hệ 1-N với RentalDetails
        public ICollection<RentalDetail> RentalDetails { get; set; }
    }
}