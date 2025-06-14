using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.MaintenanaceExpense
{
    public class DetailsModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public DetailsModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

      public MaintenanceExpenses MaintenanceExpenses { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MaintenanceExpenses == null)
            {
                return NotFound();
            }

            var maintenanceexpenses = await _context.MaintenanceExpenses.FirstOrDefaultAsync(m => m.ID == id);
            if (maintenanceexpenses == null)
            {
                return NotFound();
            }
            else 
            {
                MaintenanceExpenses = maintenanceexpenses;
            }
            return Page();
        }
    }
}
