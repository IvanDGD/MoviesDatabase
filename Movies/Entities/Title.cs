using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies.Entities
{
    public class Title
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public override string ToString()
        {
            return $"{Id}. {Name}, {Duration}";
        }

    }
}
