using System.ComponentModel.DataAnnotations;

namespace Project_00.Dtos
{
    public class UserDto
    {
        [Required]
        [EmailAddress]
        public required string Username { get; set; }
        [MinLength(4)]
        [Required]
        public required string Password { get; set; }
    }
}
