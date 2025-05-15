using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDAPP.Data;
using CRUDAPP.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPP.Pages.Funds
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
            int id = Convert.ToInt32(Request.Cookies["logindetail"]);
            var list = _context.Member.Where(c => c.groupId == id).ToList();
            Memebers = new List<SelectListItem>();
            Year = new List<SelectListItem>();
            foreach (var item in list)
            {
                Memebers.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.FlatNumber +"-"+ item.Name });
            }
            for (int i = 2022; i < 2030; i++)
            {
                Year.Add(new SelectListItem { Value = i.ToString(), Text =i.ToString() });
            }
           
            var listFest = _context.Festival.ToList();
            Festives = new List<SelectListItem>();
            foreach (var item in listFest)
            {
                Festives.Add(new SelectListItem { Value = item.ID.ToString(), Text =  item.Name });
            }
            return Page();
        }

        [BindProperty]
        public Fund Fund { get; set; } = default!;
        public List<SelectListItem> Memebers { get; set; }
        public List<SelectListItem> Festives { get; set; }
        public List<SelectListItem> Year { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            int id = Convert.ToInt32(Request.Cookies["logindetail"]);
            Fund.groupId = id;
            Fund.description = Fund.Amount.ToString() +" given to "+ Fund.givenToName;
            if (!ModelState.IsValid || _context.Fund == null || Fund == null)
            {
                return Page();
            }
           Fund.TotalAmount = Fund.Amount+Fund.FoodAmount;
            _context.Fund.Add(Fund);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
