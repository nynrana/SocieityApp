using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDAPP.Data;
using CRUDAPP.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace CRUDAPP.Pages.ExpenseDetail
{
    public class CreateModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Year = new List<SelectListItem>();
            for (int i = 2022; i < 2030; i++)
            {
                Year.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            var listFest = _context.Festival.ToList();
            Festives = new List<SelectListItem>();
            foreach (var item in listFest)
            {
                Festives.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            return Page();
        }

        [BindProperty]
        public Expenses Expenses { get; set; } = default!;
        public List<SelectListItem> Festives { get; set; }
        public List<SelectListItem> Year { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Expenses == null || Expenses == null)
            {
                return Page();
            }
            int id = Convert.ToInt32(Request.Cookies["logindetail"]);   
            Expenses.groupId =id;
            _context.Expenses.Add(Expenses);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
