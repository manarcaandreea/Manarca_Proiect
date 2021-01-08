using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Manarca_Proiect.Data;

namespace Manarca_Proiect.Models
{

    public class MovieGenrePageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(Manarca_ProiectContext context,
        Movie movie)
        {
            var allCategories = context.Genre;
            var MovieGenre = new HashSet<int>(
            movie.MovieGenres.Select(c => c.MovieID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    GenreID = cat.ID,
                    Name = cat.GenreName,
                    Assigned = MovieGenre.Contains(cat.ID)
                });
            }
        }
        public void UpdateMovieGenres(Manarca_ProiectContext context,
        string[] selectedCategories, Movie movieToUpdate)
        {
            if (selectedCategories == null)
            {
                movieToUpdate.MovieGenres = new List<MovieGenre>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var MovieGenre = new HashSet<int>
            (movieToUpdate.MovieGenres.Select(c => c.Genre.ID));
            foreach (var cat in context.Genre)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!MovieGenre.Contains(cat.ID))
                    {
                        movieToUpdate.MovieGenres.Add(
                        new MovieGenre
                        {
                            MovieID = movieToUpdate.ID,
                            GenreID = cat.ID
                        });
                    }
                }
                else
                {
                    if (MovieGenre.Contains(cat.ID))
                    {
                        MovieGenre courseToRemove
                        = movieToUpdate
                        .MovieGenres
                        .SingleOrDefault(i => i.GenreID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
