using CRUDAPP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUDAPP.Pages.LoginPage
{
    public class IndexModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;
        public IndexModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
           
        }

        [BindProperty]
        public Login Login { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Register == null || Login == null)
            {
                return Page();
            }
            
            return RedirectToPage("/Member/Index");
        }

    }
}
