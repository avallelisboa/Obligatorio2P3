using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models.BL
{
    public class OBL2P3PortLogContext : DbContext
    {
        public OBL2P3PortLogContext() : base("con"){}

        public DbSet<Client> Clients { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().Property(k => k.Tin).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Import>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<User>();

            base.OnModelCreating(modelBuilder);
        }

    }
}