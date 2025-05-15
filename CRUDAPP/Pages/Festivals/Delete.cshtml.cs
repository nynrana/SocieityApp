using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.Festivals
{
    public class DeleteModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public DeleteModel(CRUDAPP.Data.CRUDAPPContext context)
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

            var festival = await _context.Festival.FirstOrDefaultAsync(m => m.ID == id);

            if (festival == null)
            {
                return NotFound();
            }
            else 
            {
                Festival = festival;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Festival == null)
            {
                return NotFound();
            }
            var festival = await _context.Festival.FindAsync(id);

            if (festival != null)
            {
                Festival = festival;
                _context.Festival.Remove(Festival);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
