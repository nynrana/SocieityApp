using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.Registration
{
    public class DeleteModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public DeleteModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Register Register { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Register == null)
            {
                return NotFound();
            }

            var register = await _context.Register.FirstOrDefaultAsync(m => m.ID == id);

            if (register == null)
            {
                return NotFound();
            }
            else 
            {
                Register = register;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Register == null)
            {
                return NotFound();
            }
            var register = await _context.Register.FindAsync(id);

            if (register != null)
            {
                Register = register;
                _context.Register.Remove(Register);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
