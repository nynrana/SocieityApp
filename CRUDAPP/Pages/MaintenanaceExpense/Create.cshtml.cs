using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.MaintenanaceExpense
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
        public MaintenanceExpenses MaintenanceExpenses { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.MaintenanceExpenses == null || MaintenanceExpenses == null)
            {
                return Page();
            }
            int id = Convert.ToInt32(Request.Cookies["logindetail"]);
            MaintenanceExpenses.groupId = id;
            _context.MaintenanceExpenses.Add(MaintenanceExpenses);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
