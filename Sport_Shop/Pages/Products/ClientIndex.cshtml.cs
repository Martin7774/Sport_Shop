using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sport_Shop.Models;

namespace Sport_Shop.Pages.Products
{
    public class ClientIndexModel : PageModel
    {
        private readonly Sport_Shop.Data.Sport_ShopContext _context;

        public ClientIndexModel(Sport_Shop.Data.Sport_ShopContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product
                .Include(p => p.Category).ToListAsync();
        }
    }
}
