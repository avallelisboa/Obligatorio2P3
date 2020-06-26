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
            modelBuilder.Entity<Client>().Property(p => p.Tin).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Client>().Property(p => p.Name).HasMaxLength(30);
            modelBuilder.Entity<Client>().Property(p => p.Seniority).IsRequired();
            modelBuilder.Entity<Client>().Property(p => p.Discount).IsRequired();
            modelBuilder.Entity<Client>().Property(p => p.RegisterDate).IsRequired();

            modelBuilder.Entity<Import>().Property(p => p.ProductId).IsRequired();
            modelBuilder.Entity<Import>().Property(p => p.PriceByUnit).IsRequired();
            modelBuilder.Entity<Import>().Property(p => p.Ammount).IsRequired();
            modelBuilder.Entity<Import>().Property(p => p.EntryDate).IsRequired();
            modelBuilder.Entity<Import>().Property(p => p.DepartureDate).IsRequired();
            modelBuilder.Entity<Import>().Property(p => p.IsStored).IsRequired();

            modelBuilder.Entity<Product>().Property(p => p.ProductId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Product>().Property(p => p.ProductId).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Weight).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.ClientTin).IsRequired();

            modelBuilder.Entity<User>().Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<User>().Property(p => p.Password).HasMaxLength(30);
            modelBuilder.Entity<User>().Property(p => p.Role).HasMaxLength(10);

            base.OnModelCreating(modelBuilder);
        }

    }
}