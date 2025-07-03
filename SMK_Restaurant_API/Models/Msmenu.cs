using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Models
{
    public class Msmenu
    {
        [Key]
        public int MenuID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Price { get; set; }

        [Required]
        public string PhotoUrl { get; set; } = string.Empty ;

        [Required]
        public string Photo { get; set; } = string.Empty;
    }
}
