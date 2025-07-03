using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Models
{
    public class OrderPackage
    {
        [Key]
        public int OrderPackageID { get; set; }

        [Required]
        public string OrderID { get; set; } = string.Empty;

        [Required]
        public int PackageID { get; set; }

        [Required]
        public int PortionQty { get; set; }
    }

}
