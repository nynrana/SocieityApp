using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.Members
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
            return Page();
        }

        [BindProperty]
        public Member Member { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Member == null || Member == null)
            {
                return Page();
            }
            int id = Convert.ToInt32(Request.Cookies["logindetail"]);
            Member.groupId = id;
            _context.Member.Add(Member);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
