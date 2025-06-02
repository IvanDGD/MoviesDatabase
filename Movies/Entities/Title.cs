using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Entities
{
    public class Title
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public int Year { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Name} ({Year}) - {Description}";
        }
    }
}
