using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.MaintenanceDetail
{
    public class DetailsModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public DetailsModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

      public Maintenance Maintenance { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Maintenance == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenance.FirstOrDefaultAsync(m => m.ID == id);
            if (maintenance == null)
            {
                return NotFound();
            }
            else 
            {
                Maintenance = maintenance;
            }
            return Page();
        }
    }
}
