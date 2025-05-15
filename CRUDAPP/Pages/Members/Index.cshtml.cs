using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;

namespace CRUDAPP.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public IndexModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }

        public IList<Member> Member { get;set; } = default!;
        public Int32 TotalMembers { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Member != null)
            {
                int id = Convert.ToInt32(Request.Cookies["logindetail"]);
                Member = await _context.Member.Where(c=> c.groupId==id).OrderBy(c=> c.FlatNumber).ToListAsync();
                TotalMembers = Member.Count();
            }
        }
    }
}
