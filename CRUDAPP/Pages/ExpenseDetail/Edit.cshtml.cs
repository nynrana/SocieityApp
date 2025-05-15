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

namespace CRUDAPP.Pages.ExpenseDetail
{
    public class EditModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public EditModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expenses Expenses { get; set; } = default!;
        public List<SelectListItem> Festives { get; set; }
        public List<SelectListItem> Year { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expenses =  await _context.Expenses.FirstOrDefaultAsync(m => m.ID == id);
            if (expenses == null)
            {
                return NotFound();
            }
            int ids = Convert.ToInt32(Request.Cookies["logindetail"]);
            expenses.groupId = ids;
            Expenses = expenses;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Expenses).State = EntityState.Modified;
            int ids = Convert.ToInt32(Request.Cookies["logindetail"]);
            Expenses.groupId = ids;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpensesExists(Expenses.ID))
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

        private bool ExpensesExists(int id)
        {
          return (_context.Expenses?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
