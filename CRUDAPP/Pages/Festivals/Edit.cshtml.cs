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

namespace CRUDAPP.Pages.Festivals
{
    public class EditModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public EditModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Festival Festival { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Festival == null)
            {
                return NotFound();
            }

            var festival =  await _context.Festival.FirstOrDefaultAsync(m => m.ID == id);
            if (festival == null)
            {
                return NotFound();
            }
            Festival = festival;
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

            _context.Attach(Festival).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FestivalExists(Festival.ID))
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

        private bool FestivalExists(int id)
        {
          return (_context.Festival?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
