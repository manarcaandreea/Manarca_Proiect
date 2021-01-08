using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Manarca_Proiect.Models;

namespace Manarca_Proiect.Data
{
    public class Manarca_ProiectContext : DbContext
    {
        public Manarca_ProiectContext (DbContextOptions<Manarca_ProiectContext> options)
            : base(options)
        {
        }

        public DbSet<Manarca_Proiect.Models.Movie> Movie { get; set; }

        public DbSet<Manarca_Proiect.Models.Producer> Producer { get; set; }

        public DbSet<Manarca_Proiect.Models.Genre> Genre { get; set; }

        public DbSet<Manarca_Proiect.Models.MovieGenre> MovieGenre { get; set; }
    }
}
