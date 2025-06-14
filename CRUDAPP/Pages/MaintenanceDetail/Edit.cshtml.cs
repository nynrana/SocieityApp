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

namespace CRUDAPP.Pages.MaintenanceDetail
{
    public class EditModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public EditModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Maintenance Maintenance { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Maintenance == null)
            {
                return NotFound();
            }

            var maintenance =  await _context.Maintenance.FirstOrDefaultAsync(m => m.ID == id);
            if (maintenance == null)
            {
                return NotFound();
            }
            Maintenance = maintenance;
            ViewData["members"] = new SelectList(_context.Member, "ID", "Name");
            
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

            _context.Attach(Maintenance).State = EntityState.Modified;

            try
            {
                int id = Convert.ToInt32(Request.Cookies["logindetail"]);
                Maintenance.groupId = id;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceExists(Maintenance.ID))
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

        private bool MaintenanceExists(int id)
        {
          return (_context.Maintenance?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
