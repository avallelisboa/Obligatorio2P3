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
    public class UserRepository : IRepository<User>
    {
        private OBL2P3PortLogContext db = new OBL2P3PortLogContext();

        public bool Add(User instance)
        {
            var result = instance.isUserValid();

            if (!result.Item1) return false;
            else
            {
                try
                {
                    db.Users.Add(instance);
                    db.SaveChanges();

                    return true;
                }
                catch (Exception err)
                {
                    return false;
                }
            }
        }

        public List<User> FindAll()
        {
            try
            {
                List<User> users = db.Users.ToList();
                return users;
            }
            catch(Exception err)
            {
                return null;
            }
        }

        public User FindById(object id)
        {
            try
            {
                User user = db.Users.Find(id);
                return user;
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
                var user = db.Users.Find(id);
                if (user == null) return false;

                db.Users.Remove(user);
                db.SaveChanges();

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool Update(User instance)
        {
            if (instance == null) return false;

            try
            {
                var user = db.Users.Find(instance.Id);
                var result = user.SetPassword(instance.Password);

                if (!result.Item1) return false;

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