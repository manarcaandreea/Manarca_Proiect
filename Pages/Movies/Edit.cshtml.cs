using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Manarca_Proiect.Data;
using Manarca_Proiect.Models;

namespace Manarca_Proiect.Pages.Movies
{
    public class EditModel : MovieGenrePageModel
    {
        private readonly Manarca_Proiect.Data.Manarca_ProiectContext _context;

        public EditModel(Manarca_Proiect.Data.Manarca_ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie
                .Include(b => b.Producer)
                .Include(b => b.MovieGenres).ThenInclude(b => b.Genre)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Movie);
            ViewData["ProducerID"] = new SelectList(_context.Set<Producer>(), "ID", "ProducerName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
            selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movieToUpdate = await _context.Movie
                .Include(i => i.Producer)
                .Include(i => i.MovieGenres)
                    .ThenInclude(i => i.Genre)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (await TryUpdateModelAsync<Movie>(
                movieToUpdate,
                "Movie",
                i => i.Title, i => i.Director,
                i => i.Price, i => i.ProductionDate, i => i.Producer))
            {
                UpdateMovieGenres(_context, selectedCategories, movieToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateMovieGenres(_context, selectedCategories, movieToUpdate);
            PopulateAssignedCategoryData(_context, movieToUpdate);
            return Page();
        }
    }
}

       /*     if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
*/