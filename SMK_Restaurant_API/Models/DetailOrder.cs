using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Models
{
    public class DetailOrder
    {
        [Key]
        public int DetailID { get; set; }
        public string OrderID { get; set; } = string.Empty;
        public int MenuID { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public string status { get; set; } = string.Empty;


    }
}
