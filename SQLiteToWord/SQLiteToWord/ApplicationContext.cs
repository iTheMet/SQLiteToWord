using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SQLiteToWord
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Products> Products { get; set; }

        public ApplicationContext() : base("DefaultConnection") {}
    }
}
