using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Versioning;

namespace CRUDAPP.Pages.Funds
{
    public class IndexModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public IndexModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        public IList<Fund> Fund { get; set; } = default!;
        public IList<Member> Member { get; set; } = default!;
        public List<SelectListItem> Year { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalMember{ get; set; }
        public List<SelectListItem> Festives { get; set; }
        //public int fundForYear { get; set; } = DateTime.Now.Year;
        public async Task OnGetAsync()
        {
            var listFest = _context.Festival.ToList();
            Festives = new List<SelectListItem>();
            Festives.Add(new SelectListItem { Value = "0", Text = "- Select Festival-" });
            foreach (var item in listFest)
            {
                Festives.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            FundForm = new Fund();
            FundForm.fundForYear = DateTime.Now.Year;
            Year = new List<SelectListItem>();
            for (int i = 2022; i < 2030; i++)
            {
                Year.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            int id = Convert.ToInt32(Request.Cookies["logindetail"]);
            if (_context.Fund != null)
            {                
                Fund = await _context.Fund.Where(c => c.groupId == id && c.fundForYear == FundForm.fundForYear).ToListAsync();
                Fund.ToList().ForEach(c => c.givenToName = _context.Member.Single(d => d.ID == c.givenTo).FlatNumber + "- " + _context.Member.Single(d => d.ID == c.givenTo).Name);
                Fund.ToList().ForEach(c => c.givenByName = _context.Member.Single(d => d.ID == c.givenBy).FlatNumber + "- " + _context.Member.Single(d => d.ID == c.givenBy).Name);
            }
            TotalMember = Fund.Count();
            TotalAmount = Fund.Sum(c => c.Amount);
            var memberIds = Fund.Where(x=> x.groupId == id).Select(x => x.givenBy).ToArray();            
            Member = await _context.Member.Where(x => x.groupId == id && !memberIds.Contains(x.ID)).ToListAsync();
        }
        [BindProperty]
        public Fund FundForm { get; set; } = default!;
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
            if (_context.Fund != null)
            {                
                Fund = await _context.Fund.Where(c => c.groupId == id && c.fundForYear == FundForm.fundForYear).ToListAsync();
                Fund.ToList().ForEach(c => c.givenToName = _context.Member.Single(d => d.ID == c.givenTo).FlatNumber + "- " + _context.Member.Single(d => d.ID == c.givenTo).Name);
                Fund.ToList().ForEach(c => c.givenByName = _context.Member.Single(d => d.ID == c.givenBy).FlatNumber + "- " + _context.Member.Single(d => d.ID == c.givenBy).Name);
            }
            if (!string.IsNullOrEmpty(FundForm.givenToName))
                Fund = Fund.Where(c => c.givenByName.ToLower().Contains(FundForm.givenToName.ToLower()) || c.givenToName.ToLower().Contains(FundForm.givenToName.ToLower())).ToList();
            if (FundForm.festivalId > 0)
                Fund = Fund.Where(c => c.festivalId == FundForm.festivalId).ToList();
            TotalAmount = Fund.Sum(c => c.Amount);
            TotalMember = Fund.Count();
            Year = new List<SelectListItem>();
            for (int i = 2022; i < 2030; i++)
            {
                Year.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            var memberIds = Fund.Where(x => x.groupId == id).Select(x => x.givenBy).ToArray();
            Member = await _context.Member.Where(x => x.groupId == id && !memberIds.Contains(x.ID)).ToListAsync();
            return Page();
        }
    }
}
