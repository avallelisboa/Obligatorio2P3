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
    public class ClientRepository : IRepository<Client>
    {
        private OBL2P3PortLogContext db = new OBL2P3PortLogContext();

        public bool Add(Client instance)
        {
            if (!instance.isTinValid()) return false;

            try
            {
                db.Clients.Add(instance);
                db.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public IEnumerable<Client> FindAll()
        {
            try
            {
                var clients = db.Clients.ToList();
                return clients;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        public Client FindById(object id)
        {
            try
            {
                var client = db.Clients.Find(id);
                return client;
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
                var client = db.Clients.Find(id);
                db.Clients.Remove(client);
                db.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool Update(Client instance)
        {
            if (instance == null) return false;

            try
            {
                var client = db.Clients.Find(instance.Tin);
                client.Name = instance.Name;
                client.Seniority = instance.Seniority;
                client.Products = client.Products;

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