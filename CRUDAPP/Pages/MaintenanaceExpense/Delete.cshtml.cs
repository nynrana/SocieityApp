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
    public class DeleteModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public DeleteModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MaintenanceExpenses == null)
            {
                return NotFound();
            }
            var maintenanceexpenses = await _context.MaintenanceExpenses.FindAsync(id);

            if (maintenanceexpenses != null)
            {
                MaintenanceExpenses = maintenanceexpenses;
                _context.MaintenanceExpenses.Remove(MaintenanceExpenses);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
