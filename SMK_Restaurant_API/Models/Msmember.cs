using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Models
{
    public class Msmember
    {
        [Key]
        [StringLength(8)]
        public string MemberID { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Email { get; set; }  = string.Empty;

        [Required, StringLength(13)]
        public string Handphone { get; set; } = string.Empty;

        public DateTime JoinDate { get; set; } 

        [Required]
        public string Password { get; set; } = string.Empty;


        public ICollection<Review>? Reviews { get;set; }
    }

}
