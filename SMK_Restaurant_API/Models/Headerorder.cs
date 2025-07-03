using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Models
{
    public class Headerorder
    {
        [Key]
        public string OrderID { get; set; } = string.Empty;

        [Required]
        public string EmployeeID { get; set; }  = string.Empty ;

        [Required]
        public string MemberID { get; set;} = string.Empty ;

        [Required]
        public string Date {  get; set; } = string.Empty ;


        [Required]
        public string Payment { get; set; } = string.Empty;

        [Required]
        public string Bank { get; set; } = string.Empty;


        [Required]
        public string PaymentStatus { get; set; } = string.Empty;

        [Required]
        public string CardNumber { get; set; } = string.Empty;

    }
}
