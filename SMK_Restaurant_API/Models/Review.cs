using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMK_Restaurant_API.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        [ForeignKey("Member")]
        public int MenuID { get; set; }

        [Required]
        public string MemberID { get; set; } = string.Empty;

        [Required]
        public int Rating { get; set; }

        public string ReviewText { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public Msmember? Member { get; set; } 
    }

}
