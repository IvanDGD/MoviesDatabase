using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Title> Movies { get; set; } = new List<Title>();

        public override string ToString()
        {
            return $"{Id}. {Username} - {Email}";
        }
    }
}
