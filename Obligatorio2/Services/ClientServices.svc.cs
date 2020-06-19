using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Obligatorio2.Models.BL;
using Obligatorio2.Models.Repositories;

namespace Obligatorio2.Services
{
    public class ClientServices : IClientServices
    {
        public IEnumerable<ClientDTO> GetClients()
        {
            IRepository<Client> clientsRepository = new ClientRepository();
            var clients = clientsRepository.FindAll();

            List<ClientDTO> clientsList = new List<ClientDTO>();
            foreach (var c in clients)
            {
                clientsList.Add(new ClientDTO(c.Tin, c.Name, c.Discount, c.Seniority, c.RegisterDate));
            }
            
            return clientsList;
        }

        public int GetExpectedIncome(long tin)
        {
            IRepository<Client> clientRepository = new ClientRepository();            
            ImportRepository importsRepository = new ImportRepository();

            var client = clientRepository.FindById(tin);
            var imports = importsRepository.FindByClientId(tin);

            uint discount = client.Discount;

            int expectedIncome = 0;
            
            foreach(var i in imports)
            {
                if (i.IsStored)
                {
                    var elapsed = i.DepartureDate.Subtract(i.EntryDate);
                    int days = Convert.ToInt32(elapsed.TotalDays);
                    int income = 2 * (Convert.ToInt32(i.PriceByUnit)) / (Convert.ToInt32(100 - discount));
                    income = income * days;
                    expectedIncome += income;
                }
            }

            return expectedIncome;
        }
    }
}
