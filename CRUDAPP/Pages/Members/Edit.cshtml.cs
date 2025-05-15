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
using System.Diagnostics.Metrics;

namespace CRUDAPP.Pages.Members
{
    public class EditModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public EditModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member =  await _context.Member.FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }
            int ids = Convert.ToInt32(Request.Cookies["logindetail"]);
            member.groupId = ids;
            Member = member;
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

            _context.Attach(Member).State = EntityState.Modified;
            int ids = Convert.ToInt32(Request.Cookies["logindetail"]);
            Member.groupId = ids;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(Member.ID))
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

        private bool MemberExists(int id)
        {
          return (_context.Member?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
