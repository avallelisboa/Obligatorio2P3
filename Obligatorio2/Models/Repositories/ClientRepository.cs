using Obligatorio2.Models.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Obligatorio2.Models.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        public bool Add(Client instance)
        {
            bool isTinValid = instance.isTinValid();

            if (!isTinValid) return false;
            else
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO Clients(Tin, ClientName, Discount, Seniority, RegisterDate) VALUES(@tin, @name, @discount, @seniority, @registerDate)", con);

                    SqlParameter _tin = new SqlParameter("@tin", instance.Tin);
                    SqlParameter _name = new SqlParameter("@name", instance.Name);
                    SqlParameter _discount = new SqlParameter("@discount", instance.Discount);
                    SqlParameter _seniority = new SqlParameter("@seniority", instance.Seniority);
                    SqlParameter _registerDate = new SqlParameter("@registerDate", instance.RegisterDate);

                    command.Parameters.Add(_tin);
                    command.Parameters.Add(_name);
                    command.Parameters.Add(_discount);
                    command.Parameters.Add(_seniority);
                    command.Parameters.Add(_registerDate);

                    var result = command.ExecuteNonQuery();

                    con.Close();

                    if (result == 1) return true;
                    else return false;
                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }

        public IEnumerable<Client> FindAll()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Clients", con);
                var result = command.ExecuteReader();

                List<Client> clientsList = new List<Client>();

                while (result.Read())
                {
                    long _tin = Convert.ToInt64(result["Tin"]);
                    string _name = Convert.ToString(result["ClientName"]);
                    DateTime _registerDate = Convert.ToDateTime(result["RegisterDate"]);
                    uint _discount = Convert.ToUInt32(result["Discount"]);
                    uint _seniority = Convert.ToUInt32(result["Seniority"]);
                    Client client = new Client(_name, _tin,_discount,_seniority, _registerDate);
                    clientsList.Add(client);
                }

                con.Close();

                return clientsList;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Client FindById(object id)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Clients WHERE Clients.Tin = @tin", con);

                SqlParameter tin = new SqlParameter("@tin", id);
                command.Parameters.Add(tin);

                var result = command.ExecuteReader();
                string _name = ""; long _tin = 0;DateTime _registerDate = new DateTime();
                uint _discount = 0;

                if (result.Read())
                {
                    _name = Convert.ToString(result["ClientName"]);
                    _tin = Convert.ToInt64(result["Tin"]);
                    _discount = Convert.ToUInt32(result["Discount"]);
                    _registerDate = Convert.ToDateTime(result["RegisterDate"]);
                }

                Client _client = new Client(_name, _tin,_discount, _registerDate);
                

                con.Close();
                return _client;
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

                SqlCommand command = new SqlCommand("DELETE * FROM Clients WHERE Clients.Id = @id", con);

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

        public bool Update(Client instance)
        {
            if (instance == null) return false;
            else
            {
                long tin = instance.Tin;
                string name = instance.Name;
                uint seniority = instance.Seniority;
                uint discount = instance.Discount;

                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionString);

                    SqlCommand command = new SqlCommand("UPDATE Clients SET Tin = @tin, Name = @name, Discount = @discount, Senniority = @seniority WHERE Clients.Tin = @tin", con);

                    SqlParameter _tin = new SqlParameter("@tin", tin);
                    SqlParameter _name = new SqlParameter("@name", name);
                    SqlParameter _seniority = new SqlParameter("@seniority", seniority);
                    SqlParameter _discount = new SqlParameter("@discount", discount);

                    command.Parameters.Add(_tin);
                    command.Parameters.Add(_name);
                    command.Parameters.Add(_seniority);
                    command.Parameters.Add(_discount);

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
        }
    }
}