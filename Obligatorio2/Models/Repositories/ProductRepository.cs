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
    public class ProductRepository : IRepository<Product>
    {
        private OBL2P3PortLogContext db = new OBL2P3PortLogContext();

        public bool Add(Product instance)
        {
            try
            {
                db.Products.Add(instance);
                db.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
            
        }

        public List<Product> FindAll()
        {
            try
            {
                List<Product> products = db.Products.ToList();

                products.ForEach(p => {
                    List <Import> _imports = db.Imports.Where(i => i.ProductId == p.ProductId && i.IsStored).ToList();
                    if(_imports != null) p.Ammount = _imports.Sum(i => i.Ammount);
                });
       
                return products;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Product FindById(object id)
        {
            try
            {
                Product product = db.Products.Find(id);
                List<Import> _imports = db.Imports.Where(i => i.ProductId == product.ProductId).ToList();
                if (_imports != null) product.Ammount = _imports.Sum(i => i.Ammount);
                else product.Ammount = 0;
                return product;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool Remove(object id)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);

                db.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool Update(Product instance)
        {
            if (instance == null) return false;

            try
            {
                var product = db.Products.Find(instance.ProductId);

                product.Importer = instance.Importer;
                product.Weight = instance.Weight;
                product.Name = instance.Name;

                db.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}