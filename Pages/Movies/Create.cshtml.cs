using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Manarca_Proiect.Data;
using Manarca_Proiect.Models;

namespace Manarca_Proiect.Pages.Movies
{
    public class CreateModel : MovieGenrePageModel
    {
        private readonly Manarca_Proiect.Data.Manarca_ProiectContext _context;

        public CreateModel(Manarca_Proiect.Data.Manarca_ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ProducerID"] = new SelectList(_context.Set<Producer>(), "ID", "ProducerName");
            var movie = new Movie();
            movie.MovieGenres = new List<MovieGenre>();
            PopulateAssignedCategoryData(_context, movie);
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newMovie = new Movie(); if (selectedCategories != null)
            {
                newMovie.MovieGenres = new List<MovieGenre>(); foreach (var cat in selectedCategories)
                {
                    var catToAdd = new MovieGenre
                    {
                        GenreID = int.Parse(cat)
                    }; newMovie.MovieGenres.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Movie>(newMovie, "Movie", i => i.Title, i => i.Director, i => i.Price, i => i.ProductionDate, i => i.ProducerID)) { _context.Movie.Add(newMovie); await _context.SaveChangesAsync(); return RedirectToPage("./Index"); }
            PopulateAssignedCategoryData(_context, newMovie); return Page();
        }
    }


    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://aka.ms/RazorPagesCRUD.
}