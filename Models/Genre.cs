using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manarca_Proiect.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string GenreName { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
