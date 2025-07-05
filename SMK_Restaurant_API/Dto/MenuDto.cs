using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Dto
{
    public class MenuDto
    {
        public int MenuID { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Price { get; set; }

        public string PhotoUrl { get; set; } = string.Empty;
    }
}
