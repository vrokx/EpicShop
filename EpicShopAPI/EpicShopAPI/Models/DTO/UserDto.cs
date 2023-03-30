using System.ComponentModel.DataAnnotations;

namespace EpicShopAPI.Models.DTO
{
    public class UserDto
    {

        [Required]
        public string FullName { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
