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
        private OBL2P3PortLogContext _dbContext = new OBL2P3PortLogContext();

        public bool Add(User instance)
        {
            var result = instance.isUserValid();

            if (!result.Item1) return false;
            else
            {
                try
                {
                    _dbContext.Users.Add(instance);
                    _dbContext.SaveChanges();

                    return true;
                }
                catch (Exception err)
                {
                    return false;
                }
            }
        }

        public IEnumerable<User> FindAll()
        {
            try
            {
                var users = _dbContext.Users.ToList();
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
                int _id = Convert.ToInt32(id);
                var user = _dbContext.Users.Find(id);
                return user;
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
                var user = _dbContext.Users.Find(id);
                if (user == null) return false;

                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();

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
                var user = _dbContext.Users.Find(instance.Id);
                var result = user.SetPassword(instance.Password);

                if (!result.Item1) return false;

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