using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.MaintenanaceExpense
{
    public class EditModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public EditModel(CRUDAPP.Data.CRUDAPPContext context)
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

            var maintenanceexpenses =  await _context.MaintenanceExpenses.FirstOrDefaultAsync(m => m.ID == id);
            if (maintenanceexpenses == null)
            {
                return NotFound();
            }
            MaintenanceExpenses = maintenanceexpenses;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MaintenanceExpenses).State = EntityState.Modified;

            try
            {
                int id = Convert.ToInt32(Request.Cookies["logindetail"]);
                MaintenanceExpenses.groupId = id;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceExpensesExists(MaintenanceExpenses.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MaintenanceExpensesExists(int id)
        {
          return (_context.MaintenanceExpenses?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
