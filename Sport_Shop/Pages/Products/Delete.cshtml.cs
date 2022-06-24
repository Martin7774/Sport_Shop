using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sport_Shop.Data;
using Sport_Shop.Models;

namespace Sport_Shop.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly Sport_Shop.Data.Sport_ShopContext _context;

        public DeleteModel(Sport_Shop.Data.Sport_ShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product
                .Include(p => p.Category).FirstOrDefaultAsync(m => m.id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FindAsync(id);

            if (Product != null)
            {
                _context.Product.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
