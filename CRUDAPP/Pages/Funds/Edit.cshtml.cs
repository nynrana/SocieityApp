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

namespace CRUDAPP.Pages.Funds
{
    public class EditModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public EditModel(CRUDAPP.Data.CRUDAPPContext context)
        {

            _context = context;
           
        }

        [BindProperty]
        public Fund Fund { get; set; } = default!;
        public List<SelectListItem> Memebers { get; set; }
        public List<SelectListItem> Festives { get; set; }
        public List<SelectListItem> Year { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Fund == null)
            {
                return NotFound();
            }

            var fund =  await _context.Fund.FirstOrDefaultAsync(m => m.ID == id);
            if (fund == null)
            {
                return NotFound();
            }
            Fund = fund;
            int lid = Convert.ToInt32(Request.Cookies["logindetail"]);
            var list = _context.Member.Where(c=> c.groupId==lid).ToList();
            Memebers = new List<SelectListItem>();
            Year = new List<SelectListItem>();
            foreach (var item in list)
            {
                Memebers.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.FlatNumber + "-" + item.Name });
            }
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
            //Fund.FundGivenTo = _context.Member.SingleOrDefault(c => c.ID == Fund.givenTo);
            //Fund.FundGivenBy = _context.Member.SingleOrDefault(c => c.ID == Fund.givenBy);
            int lid = Convert.ToInt32(Request.Cookies["logindetail"]);
            Fund.groupId = lid;
            _context.Attach(Fund).State = EntityState.Modified;
            Fund.TotalAmount = Fund.Amount + Fund.FoodAmount;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundExists(Fund.ID))
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

        private bool FundExists(int id)
        {
          return (_context.Fund?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
