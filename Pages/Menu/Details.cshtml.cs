using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor09.efcore.Data;
using razor09.efcore.Models;

namespace _NET_project0.Pages_Menu
{
    public class DetailsModel : PageModel
    {
        private readonly razor09.efcore.Data.MenuContext _context;

        public DetailsModel(razor09.efcore.Data.MenuContext context)
        {
            _context = context;
        }

        public Menu Menu { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Menu = await _context.Menu.FirstOrDefaultAsync(m => m.ID == id);

            if (Menu == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
