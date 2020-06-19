using Obligatorio2.Models.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Obligatorio2.Models.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public bool Add(User instance)
        {
            var result = instance.isUserValid();

            if (!result.Item1) return false;
            else
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO Users(Id, UserRole, Password) VALUES(@id, @userRole, @password)", con);

                    SqlParameter id = new SqlParameter("@id", instance.Id);
                    SqlParameter userName = new SqlParameter("@userRole", instance.Role);
                    SqlParameter password = new SqlParameter("@password", instance.Password);

                    command.Parameters.Add(id);
                    command.Parameters.Add(userName);
                    command.Parameters.Add(password);

                    if(command.ExecuteNonQuery() == 1)
                    {
                        con.Close();
                        return true;
                    }

                    con.Close();
                    return false;
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
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Users", con);
                var reader = command.ExecuteReader();

                List<User> users = new List<User>();

                while (reader.Read())
                {
                    string role = Convert.ToString(reader["UserRole"]);
                    if(role == "admin")
                    {
                        users.Add(new Admin(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Password"])));
                    }
                    else
                    {
                        users.Add(new Deposit(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Password"])));
                    }
                }
                
                con.Close();

                return users;
            }
            catch(Exception err)
            {
                throw err;
            }
        }

        public User FindById(object id)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                con.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Users.Id = @id", con);

                SqlParameter _id = new SqlParameter("@id", id);    
                command.Parameters.Add(_id);

                var reader = command.ExecuteReader();

                int Id = 0; string Role="";string Password="";

                if (reader.Read())
                {
                    Id = Convert.ToInt32(reader["Id"]);
                    Role = Convert.ToString(reader["UserRole"]);
                    Password = Convert.ToString(reader["Password"]);
                }
                con.Close();

                User _user;

                if (Role == "admin") _user = new Admin(Id, Password);
                else _user = new Deposit(Id, Password);
                
                return _user;
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
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand command = new SqlCommand("DELETE * FROM Users WHERE Users.Id = @id", con);

                SqlParameter _id = new SqlParameter("@id", id);
                command.Parameters.Add(_id);

                var result = command.ExecuteNonQuery();

                con.Close();
                if (result == 1) return true;
                return false;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool Update(User instance)
        {
            if (instance == null) return false;
            else
            {
                int id = instance.Id;
                string password = instance.Password;
                string role = instance.Role;

                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    SqlCommand command = new SqlCommand("UPDATE Users SET UserRole = @Role, Password = @Password WHERE Users.Id = @id", con);

                    SqlParameter _id = new SqlParameter("@Id", id);
                    SqlParameter _role = new SqlParameter("@Role", role);
                    SqlParameter _password = new SqlParameter("@Password", password);

                    command.Parameters.Add(_id);
                    command.Parameters.Add(_role);
                    command.Parameters.Add(_password);

                    var result = command.ExecuteNonQuery();

                    con.Close();

                    if (result == 1) return true;
                    return false;
                }
                catch(Exception err)
                {
                    throw err;
                }
            }
        }
    }
}