using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Obligatorio2.Models.Repositories;

namespace Obligatorio2.Models.BL
{
    public static class Utilities
    {
        public static int digitsNumber(int n)
        {
            int d = 1;

            while (n >= 10)
            {
                n /= 10;
                d++;
            }

            return d;
        }
        public static int digitsNumber(long n)
        {
            int d = 1;

            while (n >= 10)
            {
                n /= 10;
                d++;
            }

            return d;
        }
        public static bool MakeTables()
        {
            return makeClientsTable() && makeUsersTable()
                 && makeProductsTable() && makeImportsTable();
        }

        public static bool MakeFiles()
        {
            IRepository<Client> clientRepository = new ClientRepository();
            IRepository<Import> importRepository = new ImportRepository();
            IRepository<Product> productRepository = new ProductRepository();
            IRepository<User> userRepository = new UserRepository();

            var clientsList = clientRepository.FindAll();
            var importsList = importRepository.FindAll();
            var productsList = productRepository.FindAll();
            var usersList = userRepository.FindAll();

            return makeClienstFile(clientsList) &&  
                makeImportsFile(importsList) && 
                makeProductsFile(productsList) &&
                makeUsersFile(usersList);
        }

        private static bool makeClientsTable()
        {
            string folder = "Carga";
            string fileName = "Clients.txt";

            string route = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);

            try
            {
                StreamReader file = new StreamReader(route);
                string line = file.ReadLine();

                List<Client> clientList = new List<Client>();

                while (line != null)
                {
                    string[] values = line.Split('#');

                    string name = Convert.ToString(values[0]);
                    long tin = Convert.ToInt64(values[1]);
                    uint discount = Convert.ToUInt32(values[2]);
                    uint seniority = Convert.ToUInt32(values[3]);
                    DateTime registerDate = Convert.ToDateTime(values[4]);

                    Client client = new Client(name,tin,discount,seniority,registerDate);
                    clientList.Add(client);

                    line = file.ReadLine();
                }

                IRepository<Client> clientRepository = new ClientRepository();

                foreach(var c in clientList)
                {
                    Client client = clientRepository.FindById(c.Tin);
                    if (client != null) continue;

                    clientRepository.Add(c);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
        private static bool makeImportsTable()
        {
            string folder = "Carga";
            string fileName = "Imports.txt";

            string route = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);

            try
            {
                StreamReader file = new StreamReader(route);
                string line = file.ReadLine();

                List<Import> importList = new List<Import>();

                while (line != null)
                {
                    string[] values = line.Split('#');

                    Product product = new Product(values[0]);
                    Client client = new Client(Convert.ToInt64(values[1]));
                    uint ammount = Convert.ToUInt32(values[2]);
                    int priceByUnit = Convert.ToInt32(values[3]);
                    DateTime entryDate = Convert.ToDateTime(values[4]);
                    DateTime departureDate = Convert.ToDateTime(values[5]);
                    bool isStored = Convert.ToBoolean(values[6]);

                    Import import = new Import(product, client, ammount, priceByUnit,entryDate,departureDate,isStored);
                    importList.Add(import);

                    line = file.ReadLine();
                }

                IRepository<Import> importRepository = new ImportRepository();

                foreach (var i in importList)
                {
                    Import import = importRepository.FindById(i.Id);
                    if (import != null) continue;

                    importRepository.Add(i);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static bool makeProductsTable()
        {
            string folder = "Carga";
            string fileName = "Products.txt";

            string route = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);

            try
            {
                StreamReader file = new StreamReader(route);
                string line = file.ReadLine();

                List<Product> productList = new List<Product>();

                while (line != null)
                {
                    string[] values = line.Split('#');

                    Client client = new Client(Convert.ToInt64(values[3]));
                    string id = values[0];
                    string name = values[1];
                    int weight = Convert.ToInt32(values[2]);

                    Product product = new Product(id,name,weight, client);
                    productList.Add(product);

                    line = file.ReadLine();
                }

                IRepository<Product> productRepository = new ProductRepository();

                foreach (var p in productList)
                {
                    Product product = productRepository.FindById(p.Id);
                    if (product != null) continue;

                    productRepository.Add(p);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static bool makeUsersTable()
        {
            string folder = "Carga";
            string fileName = "Users.txt";

            string route = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);

            try
            {
                StreamReader file = new StreamReader(route);
                string line = file.ReadLine();

                List<User> userList = new List<User>();

                while (line != null)
                {
                    string[] values = line.Split('#');

                    User user;
                    if(values[2] == "admin")
                    {
                        user = new Admin(Convert.ToInt32(values[0]), values[1]);
                    }
                    else
                    {
                        user = new Deposit(Convert.ToInt32(values[0]), values[1]);
                    }

                    userList.Add(user);

                    line = file.ReadLine();
                }

                IRepository<User> userRepository = new UserRepository();

                foreach (var p in userList)
                {
                    User user = userRepository.FindById(p.Id);
                    if (user != null) continue;

                    userRepository.Add(p);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool makeClienstFile(IEnumerable<Client> clients)
        {
            string folder = "Carga";
            string fileName = "Clients.txt";

            string route = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);

            try
            { 
                StreamWriter file = new StreamWriter(route,false);

                foreach (var c in clients)
                {
                    file.WriteLine($"{c.Name}#{c.Tin}#{c.Discount}#{c.Seniority}#{c.RegisterDate}#");
                }

                file.Close();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        private static bool makeImportsFile(IEnumerable<Import> imports)
        {
            string folder = "Carga";
            string fileName = "Imports.txt";

            string route = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);

            try
            {
                StreamWriter file = new StreamWriter(route, false);

                foreach (var i in imports)
                {
                    file.WriteLine($"{i.ImportedProduct.Id}#{i.ImporterClient.Tin}#{i.Ammount}#{i.PriceByUnit}#{i.EntryDate}#{i.DepartureDate}#{i.IsStored}#");
                }

                file.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static bool makeProductsFile(IEnumerable<Product> products)
        {
            string folder = "Carga";
            string fileName = "Products.txt";

            string route = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);

            try
            {
                StreamWriter file = new StreamWriter(route, false);

                foreach (var p in products)
                {
                    file.WriteLine($"{p.Id}#{p.Name}#{p.Weight}#{p.Importer.Tin}#");
                }

                file.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static bool makeUsersFile(IEnumerable<User> users)
        {
            string folder = "Carga";
            string fileName = "Users.txt";

            string route = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder, fileName);

            try
            {
                StreamWriter file = new StreamWriter(route, false);

                foreach (var u in users)
                {
                    file.WriteLine($"{u.Id}#{u.Password}#{u.Role}#");
                }

                file.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}