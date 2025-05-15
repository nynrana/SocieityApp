using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.Funds
{
    public class DetailsModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public DetailsModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

      public Fund Fund { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Fund == null)
            {
                return NotFound();
            }

            var fund = await _context.Fund.FirstOrDefaultAsync(m => m.ID == id);
            if (fund == null)
            {
                return NotFound();
            }
            else 
            {
                Fund = fund;
            }
            return Page();
        }
    }
}
