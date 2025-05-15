using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;
using NuGet.Protocol;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDAPP.Pages.ExpenseDetail
{
    public class IndexModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public IndexModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Expenses ExpensesForm { get; set; } = default!;
        public IList<Expenses> Expenses { get; set; } = default!;
        public decimal TotalExpense { get; set; }
        public decimal TotalFund { get; set; }
        public decimal RemainigFund { get; set; }
        public List<SelectListItem> Year { get; set; }
        public List<SelectListItem> Festives { get; set; }
        public async Task OnGetAsync()
        {
            var listFest = _context.Festival.ToList();
            Festives = new List<SelectListItem>();
            Festives.Add(new SelectListItem { Value = "0", Text = "- Select Festival-" });
            foreach (var item in listFest)
            {
                Festives.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            ExpensesForm = new Expenses();
            ExpensesForm.fundForYear = DateTime.Now.Year;
            Year = new List<SelectListItem>();
            for (int i = 2022; i < 2030; i++)
            {
                Year.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            if (_context.Expenses != null)
            {

                int id = Convert.ToInt32(Request.Cookies["logindetail"]);
                Expenses = await _context.Expenses.Where(c => c.groupId == id && c.fundForYear == ExpensesForm.fundForYear).ToListAsync();
                TotalExpense = Expenses.Sum(c => c.Amount);
                TotalFund = _context.Fund.Where(c => c.groupId == id && c.fundForYear == ExpensesForm.fundForYear).Sum(c => c.Amount);
                RemainigFund = TotalFund - TotalExpense;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var listFest = _context.Festival.ToList();
            Festives = new List<SelectListItem>();
            Festives.Add(new SelectListItem { Value = "0", Text = "- Select Festival-" });
            foreach (var item in listFest)
            {
                Festives.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            int id = Convert.ToInt32(Request.Cookies["logindetail"]);
            Expenses = await _context.Expenses.Where(c => c.groupId == id && c.fundForYear == ExpensesForm.fundForYear).ToListAsync();
            if (!string.IsNullOrEmpty(ExpensesForm.ExpenseName))
                Expenses = Expenses.Where(c => c.ExpenseName.ToLower().Contains(ExpensesForm.ExpenseName.ToLower())).ToList();
          
            if(ExpensesForm.festivalId> 0)
                Expenses = Expenses.Where(c => c.festivalId == ExpensesForm.festivalId).ToList();
            TotalExpense = Expenses.Sum(c => c.Amount);
            TotalFund = _context.Fund.Where(c => c.groupId == id && c.fundForYear == ExpensesForm.fundForYear ).Sum(c => c.Amount);
            RemainigFund = TotalFund - TotalExpense;
            Year = new List<SelectListItem>();
            for (int i = 2022; i < 2030; i++)
            {
                Year.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            return Page();
        }
    }
}
