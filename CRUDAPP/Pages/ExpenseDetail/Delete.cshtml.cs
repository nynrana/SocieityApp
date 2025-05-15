using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.ExpenseDetail
{
    public class DeleteModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public DeleteModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Expenses Expenses { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses.FirstOrDefaultAsync(m => m.ID == id);

            if (expenses == null)
            {
                return NotFound();
            }
            else 
            {
                Expenses = expenses;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }
            var expenses = await _context.Expenses.FindAsync(id);

            if (expenses != null)
            {
                Expenses = expenses;
                _context.Expenses.Remove(Expenses);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
