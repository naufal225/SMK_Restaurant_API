namespace SMK_Restaurant_API.Dto
{
    public class ReviewRequest
    {
        public int MenuID { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; } = string.Empty;
        public IFormFile? Photo { get; set; } 
    }

}
