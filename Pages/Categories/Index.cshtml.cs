﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Manarca_Proiect.Data;
using Manarca_Proiect.Models;

namespace Manarca_Proiect.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Manarca_Proiect.Data.Manarca_ProiectContext _context;

        public IndexModel(Manarca_Proiect.Data.Manarca_ProiectContext context)
        {
            _context = context;
        }

        public IList<Genre> Genre { get;set; }

        public async Task OnGetAsync()
        {
            Genre = await _context.Genre.ToListAsync();
        }
    }
}
