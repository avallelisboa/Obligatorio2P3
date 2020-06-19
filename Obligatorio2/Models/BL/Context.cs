using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models.BL
{
    public class Context : DbContext
    {
        public Context() : base("con"){}

        public DbSet<Client> Clients;
        public DbSet<Import> Imports;
        public DbSet<Product> Products;
        public DbSet<User> Users;
    }
}