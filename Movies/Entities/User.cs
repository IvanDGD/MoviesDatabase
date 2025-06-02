using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<Title> Movies { get; set; } = new List<Title>();

        public override string ToString()
        {
            return $"{Id}. {Username} - {Email}";
        }
    }
}
