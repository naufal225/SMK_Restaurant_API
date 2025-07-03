using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public string OrderID { get; set; } = string.Empty;

        [Required]
        public int MenuID { get; set; }

        [Required]
        public int Rating { get; set; }

        public string ReviewText { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
