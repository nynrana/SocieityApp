using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUDAPP.Model;

namespace CRUDAPP.Data
{
    public class CRUDAPPContext : DbContext
    {
        public CRUDAPPContext (DbContextOptions<CRUDAPPContext> options)
            : base(options)
        {
        }

        public DbSet<CRUDAPP.Model.User> User { get; set; } = default!;

        public DbSet<CRUDAPP.Model.Member>? Member { get; set; }

        public DbSet<CRUDAPP.Model.Fund>? Fund { get; set; }

        public DbSet<CRUDAPP.Model.Expenses>? Expenses { get; set; }

        public DbSet<CRUDAPP.Model.Register>? Register { get; set; }

        public DbSet<CRUDAPP.Model.Festival>? Festival { get; set; }
        public DbSet<CRUDAPP.Model.Maintenance>? Maintenance { get; set; }
    }
}
