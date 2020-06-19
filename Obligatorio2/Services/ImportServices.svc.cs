using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Obligatorio2.Models.BL;
using Obligatorio2.Models.Repositories;

namespace Obligatorio2.Services
{
    public class ImportServices : IImportServices
    {
        public bool AddImport(string productId, long tin, int priceByUnit,
            uint ammount, bool IsStored, DateTime entryDate, DateTime departureDate)
        {
            Product product = new Product(productId);
            Client client = new Client(tin);

            IRepository<Import> repository = new ImportRepository();
            Import import = new Import(product, client, ammount, priceByUnit, entryDate, departureDate, IsStored);

            if (repository.Add(import)) return true;
            else return false;
        }
    }

    [DataContract]
    public class ClientDTO
    {
        public ClientDTO(long tin, string clientName, uint discount,
            uint seniority, DateTime registerDate)
        {
            Tin = tin;ClientName = clientName; RegisterDate = registerDate;
        }

        [DataMember]
        public long Tin { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public DateTime RegisterDate { get; set; }
    }
}
