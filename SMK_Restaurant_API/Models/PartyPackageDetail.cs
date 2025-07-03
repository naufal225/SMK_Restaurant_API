using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Models
{
    public class PartyPackageDetail
    {
        [Key]
        public int PackageDetailID { get; set; }

        [Required]
        public int PackageID { get; set; }

        [Required]
        public int MenuID { get; set; }

        [Required]
        public string MenuType { get; set; } = string.Empty;
    }

}
