using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.MaintenanceDetail
{
    public class CreateModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public CreateModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["members"] = new SelectList(_context.Member, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public Maintenance Maintenance { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Maintenance == null || Maintenance == null)
            {
                return Page();
            }

            _context.Maintenance.Add(Maintenance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
