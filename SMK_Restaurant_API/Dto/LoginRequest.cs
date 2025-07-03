using System.ComponentModel.DataAnnotations;

namespace SMK_Restaurant_API.Dto
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

}
