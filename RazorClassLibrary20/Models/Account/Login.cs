using System.ComponentModel.DataAnnotations;

namespace BlazorApp20.Data
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}