using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manarca_Proiect.Data;
using Manarca_Proiect.Models;

namespace Manarca_Proiect.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly Manarca_Proiect.Data.Manarca_ProiectContext _context;

        public IndexModel(Manarca_Proiect.Data.Manarca_ProiectContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; }
        public MovieData MovieD { get; set; }
        public int MovieID { get; set; }
        public int GenreID { get; set; }
        public async Task OnGetAsync(int? id, int? categoryID)
        {
            MovieD = new MovieData();

            MovieD.Movies = await _context.Movie
            .Include(b => b.Producer)
            .Include(b => b.MovieGenres)
            .ThenInclude(b => b.Genre)
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();
            if (id != null)
            {
                MovieID = id.Value;
                Movie movie = MovieD.Movies
                .Where(i => i.ID == id.Value).Single();
                MovieD.Genres = movie.MovieGenres.Select(s => s.Genre);
            }
        }

    }
}
