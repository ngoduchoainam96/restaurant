using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor09.efcore.Data;
using razor09.efcore.Models;
using Microsoft.Extensions.DependencyInjection;

namespace _NET_project0.Pages_Menu
{
    public class Index2Model : PageModel
    {
        private readonly razor09.efcore.Data.MenuContext _context;

        public Index2Model(razor09.efcore.Data.MenuContext context)
        {
            _context = context;
        }

        public IList<Menu> Menu { get;set; }
         public int totalPages {set; get;}
        public int pageNumber {set;get;}

        public async Task OnGetAsync()
        {
            Menu = await _context.Menu.ToListAsync();
            if (pageNumber == 0) 
                pageNumber = 1; 

           
        }
         
         
    }
}
