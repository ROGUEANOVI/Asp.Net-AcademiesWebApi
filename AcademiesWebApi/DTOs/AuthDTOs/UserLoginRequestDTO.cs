using System.ComponentModel.DataAnnotations;

namespace AcademiesWebApi.DTOs.AuthDTOs
{
    public class UserLoginRequestDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
