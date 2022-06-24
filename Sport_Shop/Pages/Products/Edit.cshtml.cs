using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sport_Shop.Data;
using Sport_Shop.Models;

namespace Sport_Shop.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly Sport_Shop.Data.Sport_ShopContext _context;

        public EditModel(Sport_Shop.Data.Sport_ShopContext context)
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
           ViewData["categoryId"] = new SelectList(_context.Category, "id", "categoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.id))
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

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.id == id);
        }
    }
}
