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
using System.Globalization;

namespace CRUDAPP.Pages.MaintenanceDetail
{
    public class IndexModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public IndexModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }
        public List<SelectListItem> Year { get; set; }
        public List<SelectListItem> Month { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalMember { get; set; }
        public IList<Maintenance> Maintenance { get; set; } = default!;
        public IList<Member> Member { get; set; } = default!;
        public async Task OnGetAsync()
        {
            Year = new List<SelectListItem>();
            for (int i = 2022; i < 2030; i++)
            {
                Year.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            Month = new List<SelectListItem>();
            for (int i = 1; i < 12; i++)
            {
                Month.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)
                });
            }
            MaintanForm = new Maintenance();
            MaintanForm.fundForMonth = DateTime.Now.Month;
            MaintanForm.fundForYear = DateTime.Now.Year;
            if (_context.Maintenance != null)
            {
                Maintenance = await _context.Maintenance
                .Include(m => m.MaintenanceGivenBy).ToListAsync();
            }
            int id = Convert.ToInt32(Request.Cookies["logindetail"]);
            TotalMember = Maintenance.Count();
            TotalAmount = Maintenance.Sum(c => c.Amount);
            var memberIds = Maintenance.Where(x => x.groupId == id).Select(x => x.memberId).ToArray();
            Member = await _context.Member.Where(x => x.groupId == id && !memberIds.Contains(x.ID)).ToListAsync();
        }

        [BindProperty]
        public Maintenance MaintanForm { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            int id = Convert.ToInt32(Request.Cookies["logindetail"]);
            Year = new List<SelectListItem>();
            for (int i = 2022; i < 2030; i++)
            {
                Year.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            Month = new List<SelectListItem>();
            for (int i = 1; i < 12; i++)
            {
                Month.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)
                });
            }
            if (_context.Maintenance != null)
            {
                Maintenance = await _context.Maintenance
                .Include(m => m.MaintenanceGivenBy).Where(x=> x.MaintenanceDate.Year == MaintanForm.fundForYear && x.MaintenanceDate.Month == MaintanForm.fundForMonth).ToListAsync();
            }
            var memberIds = Maintenance.Where(x => x.groupId == id ).Select(x => x.memberId).ToArray();
            Member = await _context.Member.Where(x => x.groupId == id && !memberIds.Contains(x.ID)).ToListAsync();
            TotalMember = Maintenance.Count();
            TotalAmount = Maintenance.Sum(c => c.Amount);
            return Page();
        }

    }
}
