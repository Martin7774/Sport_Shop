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
    public class IndexModel : PageModel
    {
        private readonly Sport_Shop.Data.Sport_ShopContext _context;

        public IndexModel(Sport_Shop.Data.Sport_ShopContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

      

        public async Task OnGetAsync()
        {
            string currentUser = HttpContext.Request.Cookies["RoleId"];
            int int_index = Int32.Parse(currentUser);

            if (int_index != 1 || int_index != 2)
            {
                RedirectToPage("/Login/UserLogin");
            }

            Product = await _context.Product
                .Include(p => p.Category).ToListAsync();
        }
    }
}
