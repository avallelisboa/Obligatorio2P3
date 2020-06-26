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
        private OBL2P3PortLogContext _dbContext = new OBL2P3PortLogContext();

        public bool Add(Product instance)
        {
            try
            {
                _dbContext.Products.Add(instance);
                _dbContext.SaveChanges();

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
                var products = _dbContext.Products.ToList();

                products.ForEach(p => {
                    p.Ammount = Convert.ToInt32(_dbContext.Imports.Where(i=>p.ProductId == i.ProductId).Sum(i=>i.Ammount));
                });

                _dbContext.SaveChanges();
       
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
                Product product = _dbContext.Products.Find(id);
                product.Ammount = Convert.ToInt32(_dbContext.Imports.Where(i => i.ProductId == product.ProductId).Sum(i => i.Ammount));
                return product;
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
                Product product = _dbContext.Products.Find(id);
                _dbContext.Products.Remove(product);

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool Update(Product instance)
        {
            if (instance == null) return false;

            try
            {
                var product = _dbContext.Products.Find(instance.ProductId);

                product.Importer = instance.Importer;
                product.Weight = instance.Weight;
                product.Name = instance.Name;

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }
    }
}