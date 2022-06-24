using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sport_Shop.Data;
using Sport_Shop.Models;

namespace Sport_Shop.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly Sport_Shop.Data.Sport_ShopContext _context;

        public CreateModel(Sport_Shop.Data.Sport_ShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["categoryId"] = new SelectList(_context.Category, "id", "categoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
