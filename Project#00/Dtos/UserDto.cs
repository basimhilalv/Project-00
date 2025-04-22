using Project_00.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Project_00.Dtos
{
    public class UserDto
    {
        [Required]
        [EmailAddress]
        public required string Username { get; set; }
        [Required]
        [Password]
        public required string Password { get; set; }
    }

    public class UserResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public IEnumerable<User>? Data { get; set; }
        public bool Error { get; set; }
    }
    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }
            string password = value.ToString();
            if(password.Length < 8 )
            {
                ErrorMessage = "Password must be at least 8 characters long";
                return false;
            }
            if (!Regex.IsMatch(password, "[a-zA-Z]"))
            {
                ErrorMessage = "Password must contain at least one alphabet.";
                return false;
            }
            if (!Regex.IsMatch(password, "[0-9]"))
            {
                ErrorMessage = "Password must contain at least one number.";
                return false;
            }

            if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                ErrorMessage = "Password must contain at least one special character.";
                return false;
            }

            return true;
        }
    }
}

