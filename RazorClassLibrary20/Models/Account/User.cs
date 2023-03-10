using System.ComponentModel.DataAnnotations;

namespace BlazorApp20.Data
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }
        public bool IsDeleting { get; set; }
    }
}