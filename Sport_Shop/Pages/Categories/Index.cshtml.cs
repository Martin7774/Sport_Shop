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
    public class IndexModel : PageModel
    {
        private readonly Sport_Shop.Data.Sport_ShopContext _context;

        public IndexModel(Sport_Shop.Data.Sport_ShopContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
