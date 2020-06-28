using Obligatorio2.Models.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Obligatorio2.Models.Repositories
{
    public class ImportRepository : IRepository<Import>
    {
        private OBL2P3PortLogContext db = new OBL2P3PortLogContext();

        public bool Add(Import instance)
        {
            try
            {
                db.Imports.Add(instance);
                db.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<Import> FindAll()
        {
            try
            {
                List<Import> imports = db.Imports.ToList();
                return imports;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<Import> FindByClientId(object id)
        {
            try
            {
                long _id = Convert.ToInt64(id);

                var imports = db.Imports.Where(i =>
                    db.Products.Any(p => p.ProductId == i.ProductId && db.Clients.Any(c => c.Tin == p.ClientTin)))
                    .ToList();

                return imports;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public Import FindById(object id)
        {
            try
            {
                var import = db.Imports.Find(id);

                return import;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public bool Remove(object id)
        {
            try
            {
                var import = db.Imports.Find(id);
                db.Imports.Remove(import);
                db.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool Update(Import instance)
        {
            if (instance == null) return false;

            try
            {
                var import = db.Imports.Find(instance.Id);

                import.Id = instance.Id;
                import.Ammount = instance.Ammount;
                import.DepartureDate = instance.DepartureDate;
                import.EntryDate = instance.EntryDate;
                import.IsStored = instance.IsStored;
                import.ImportedProduct = instance.ImportedProduct;
                import.PriceByUnit = instance.PriceByUnit;

                if (!import.IsImportValid()) return false;

                db.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
    }
}