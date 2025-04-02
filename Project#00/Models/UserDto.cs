using System.ComponentModel.DataAnnotations;

namespace Project_00.Models
{
    public class UserDto
    {
        [Required]
        public required string Username { get; set; }
        [MinLength(4)]
        [Required]
        public required string Password { get; set; }
    }
}
