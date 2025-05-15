using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.Registration
{
    public class IndexModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public IndexModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        public IList<Register> Register { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Register != null)
            {
                Register = await _context.Register.ToListAsync();
            }
        }
    }
}
