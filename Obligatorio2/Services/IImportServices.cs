using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio2.Models.BL;
using Obligatorio2.Models.Repositories;


namespace Obligatorio2.Services
{
    [ServiceContract]
    public interface IImportServices
    {
        [OperationContract]
        bool AddImport(string productId, long tin, int priceByUnit,
            uint ammount, bool IsStored, DateTime entryDate, DateTime departureDate);
        [OperationContract]
        bool TakeImport(int Id);
        [OperationContract]
        IEnumerable<ImportDTO> GetImports();
        [OperationContract]
        IEnumerable<ImportDTO> FindImportsById(string productId);
        [OperationContract]
        IEnumerable<ImportDTO> FindImportsByName(string productName);
        [OperationContract]
        IEnumerable<ImportDTO> FindImportByClientTin(long tin);
        [OperationContract]
        IEnumerable<ImportDTO> FindImportByDate();
    }


    [DataContract]
    public class ClientDTO
    {
        public ClientDTO(long tin, string clientName, uint discount,
            uint seniority, DateTime registerDate)
        {
            Tin = tin; ClientName = clientName; RegisterDate = registerDate;
        }

        [DataMember]
        public long Tin { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public DateTime RegisterDate { get; set; }
    }

    [DataContract]
    public class ImportDTO
    {
        public ImportDTO(int id, int priceByUnit, uint ammount, DateTime entryDate, DateTime departureDate, Product imported, Client importer, bool isStored)
        {
            Id = id;
            PriceByUnit = priceByUnit;
            Ammount = ammount;
            EntryDate = entryDate;
            DepartureDate = departureDate;
            Imported = imported;
            Importer = importer;
            IsStored = isStored;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PriceByUnit { get; set; }
        [DataMember]
        public uint Ammount { get; set; }
        [DataMember]
        public DateTime EntryDate { get; set; }
        [DataMember]
        public DateTime DepartureDate { get; set; }
        [DataMember]
        public Product Imported { get; set; }
        [DataMember]
        public Client Importer { get; set; }
        [DataMember]
        public bool IsStored { get; set; }
    }
}
