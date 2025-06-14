using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.MaintenanaceExpense
{
    public class IndexModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public IndexModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        public IList<MaintenanceExpenses> MaintenanceExpenses { get;set; } = default!;
        public decimal TotalMaintenanceExpense { get; set; }
        public decimal TotalMaintenance { get; set; }
        public decimal RemainigMaintenance { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.MaintenanceExpenses != null)
            {
                int id = Convert.ToInt32(Request.Cookies["logindetail"]);
                MaintenanceExpenses = await _context.MaintenanceExpenses.Where(c => c.groupId == id).ToListAsync();
                TotalMaintenanceExpense = MaintenanceExpenses.Sum(c => c.Amount);
                TotalMaintenance = _context.Maintenance.Where(c => c.groupId == id).Sum(c => c.Amount);
                RemainigMaintenance = TotalMaintenance - TotalMaintenanceExpense;
            }
        }
    }
}
