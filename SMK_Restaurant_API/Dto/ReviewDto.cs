using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Dto
{
    public class ReviewDto
    {
        public string ReviewerName { get; set; } = string.Empty;

        public int MenuID { get; set; }

        public int Rating { get; set; }

        public string ReviewText { get; set; } = string.Empty;

        public string Photo { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
