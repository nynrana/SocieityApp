using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Data;
using CRUDAPP.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace CRUDAPP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CRUDAPP.Data.CRUDAPPContext _context;

        public IndexModel(CRUDAPP.Data.CRUDAPPContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Login Login { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Register == null || Login == null)
            {
                return Page();
            }
            Register reg = _context.Register.SingleOrDefault(c=> c.UserName == Login.UserName && c.Password ==Login.Password);
            if (reg == null)
            {
                return Page();
            }
            else
            {
                string cookie = Request.Cookies["logindetail"];
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddHours(8);
                Response.Cookies.Append("logindetail", reg.ID.ToString(), option);
                return RedirectToPage("/Members/Index");
                
            }
           
        }

    }
}
