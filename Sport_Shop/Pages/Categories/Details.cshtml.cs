using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sport_Shop.Data;
using Sport_Shop.Models;

namespace Sport_Shop.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly Sport_Shop.Data.Sport_ShopContext _context;

        public DetailsModel(Sport_Shop.Data.Sport_ShopContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
