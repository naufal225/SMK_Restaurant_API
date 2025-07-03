using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Models
{
    public class PartyPackage
    {
        [Key]
        public int PackageID { get; set; }

        [Required]
        public string PackageName { get; set; } = string.Empty;

        [Required]
        public int PricePerPortion { get; set; }

        public int MinimumPortion { get; set; } = 100;
    }

}
