using Obligatorio2.Models.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Obligatorio2.Models.Repositories
{
    public class ImportRepository : IRepository<Import>
    {
        public bool Add(Import instance)
        {            
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Imports(ProductId, Tin, PriceByUnit, Ammount, EntryDate, DepartureDate, IsStored) VALUES(@productId, @tin, @pricebyunit, @ammount, @entrydate, @departuredate, @isStored)", con);

                SqlParameter ProductId = new SqlParameter("@productId", instance.ImportedProduct.Id);
                SqlParameter Tin = new SqlParameter("@tin", instance.ImportingClient.Tin);
                SqlParameter PriceByUnit = new SqlParameter("@pricebyunit", instance.PriceByUnit);
                SqlParameter Ammount = new SqlParameter("@ammount", instance.Ammount);
                SqlParameter EntryDate = new SqlParameter("@entrydate", instance.EntryDate);
                SqlParameter DepartureDate = new SqlParameter("@departuredate", instance.DepartureDate);
                SqlParameter IsStored = new SqlParameter("@isStored", instance.IsStored);

                command.Parameters.Add(ProductId);
                command.Parameters.Add(Tin);
                command.Parameters.Add(PriceByUnit);
                command.Parameters.Add(Ammount);
                command.Parameters.Add(EntryDate);
                command.Parameters.Add(DepartureDate);
                command.Parameters.Add(IsStored);

                if (command.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }

                con.Close();
                return false;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public IEnumerable<Import> FindAll()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Imports", con);
                var reader = command.ExecuteReader();

                IRepository<Product> pRepository = new ProductRepository();
                IRepository<Client> cRepository = new ClientRepository();

                List<Import> imports = new List<Import>();
                while (reader.Read())
                {
                    imports.Add(new Import(Convert.ToInt32(reader["Id"]),pRepository.FindById(Convert.ToString(reader["ProductId"])),cRepository.FindById(Convert.ToUInt64(reader["Tin"])),Convert.ToUInt32(reader["Ammount"]),Convert.ToInt32(reader["PriceByUnit"]),Convert.ToDateTime(reader["EntryDate"]),Convert.ToDateTime(reader["DepartureDate"]),Convert.ToBoolean(reader["IsStored"])));
                }

                con.Close();

                return imports;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public IEnumerable<Import> FindByClientId(object id)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();


                SqlCommand command = new SqlCommand("SELECT * FROM Imports WHERE Tin = @tin", con);

                SqlParameter tin = new SqlParameter("@tin", id);
                command.Parameters.Add(tin);

                var result = command.ExecuteReader();

                List<Import> imports = new List<Import>();
                IRepository<Client> clRepository = new ClientRepository();
                IRepository<Product> prRepository = new ProductRepository();

                while (result.Read())
                {
                    Product _product = prRepository.FindById(Convert.ToString(result["ProductId"]));
                    Client _client = clRepository.FindById(Convert.ToInt64(result["Tin"]));
                    uint _ammount = Convert.ToUInt32(result["Ammount"]);
                    int _priceByUnit = Convert.ToInt32(result["PriceByUnit"]);
                    DateTime _entryDate = Convert.ToDateTime(result["EntryDate"]);
                    DateTime _departureDate = Convert.ToDateTime(result["DepartureDate"]);
                    bool _isStored = Convert.ToBoolean(result["IsStored"]);

                    imports.Add(new Import(_product, _client, _ammount, _priceByUnit, _entryDate, _departureDate, _isStored));
                }

                con.Close();

                return imports;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Import FindById(object id)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Import WHERE Imports.Id = @id", con);

                SqlParameter _id = new SqlParameter("@id", id);
                command.Parameters.Add(_id);

                var result = command.ExecuteReader();

                Product ImportedProduct = null; Client ImportingClient = null; uint Ammount = 0; int PriceByUnit = 0;
                DateTime EntryDate = DateTime.Now; DateTime DepartureDate = DateTime.Now; bool IsStored = true;

                var prodRepository = new ProductRepository();
                var cliRepository = new ClientRepository();

                if (result.Read())
                {
                    ImportedProduct = prodRepository.FindById(Convert.ToInt32(result["ProductId"]));
                    ImportingClient = cliRepository.FindById(Convert.ToInt32(result["Tin"]));
                    Ammount = Convert.ToUInt32(result["Ammount"]);
                    PriceByUnit = Convert.ToInt32(result["PriceByUnit"]);
                    EntryDate = Convert.ToDateTime(result["EntryDate"]);
                    DepartureDate = Convert.ToDateTime(result["DepartureDate"]);
                    IsStored = Convert.ToBoolean(result["IsStored"]);
                }

                Import _import = new Import(ImportedProduct, ImportingClient, Ammount, PriceByUnit, EntryDate, DepartureDate, IsStored);

                con.Close();
                return _import;
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

                SqlCommand command = new SqlCommand("DELETE * FROM Imports WHERE Users.Id = @id", con);

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

        public bool Update(Import instance)
        {
            if (instance == null) return false;
            else
            {
                int id = instance.Id;
                string productId = instance.ImportedProduct.Id;
                long tin = instance.ImportingClient.Tin;
                int priceByUnit = instance.PriceByUnit;
                uint ammount = instance.Ammount;
                DateTime entryDate = instance.EntryDate;
                DateTime departureDate = instance.DepartureDate;

                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();

                    SqlCommand command = new SqlCommand("UPDATE Users SET UserRole = @role, Password = @password," +
                        "PriceByUnit = @price, Ammount = @ammount, EntryDate = @entryDate, DepartureDate = @departureDate WHERE Users.Id = @id", con);

                    SqlParameter _id = new SqlParameter("@id", id);
                    SqlParameter _productId = new SqlParameter("@role", tin);
                    SqlParameter _tin = new SqlParameter("@password", productId);
                    SqlParameter _priceByUnit = new SqlParameter("@price", priceByUnit);
                    SqlParameter _ammount = new SqlParameter("@ammount", ammount);
                    SqlParameter _entryDate = new SqlParameter("@entryDate", entryDate);
                    SqlParameter _departureDate = new SqlParameter("@departureDate", departureDate);

                    command.Parameters.Add(_id);
                    command.Parameters.Add(_productId);
                    command.Parameters.Add(_tin);
                    command.Parameters.Add(_priceByUnit);
                    command.Parameters.Add(_ammount);
                    command.Parameters.Add(_entryDate);
                    command.Parameters.Add(_departureDate);

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