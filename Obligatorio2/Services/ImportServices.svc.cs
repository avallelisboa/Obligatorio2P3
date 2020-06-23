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

        public IEnumerable<ImportDTO> FindImportByClientTin(long tin)
        {
            IRepository<Import> repository = new ImportRepository();
            var imports = repository.FindAll().Where(i => i.ImporterClient.Tin == tin);
            List<ImportDTO> importDTOs = new List<ImportDTO>();

            foreach(var i in imports)
            {
                importDTOs.Add(new ImportDTO(i.Id, i.PriceByUnit, i.Ammount, i.EntryDate, i.DepartureDate, i.ImportedProduct, i.ImporterClient, i.IsStored));
            }

            return importDTOs;
        }

        public IEnumerable<ImportDTO> FindImportsById(string productId)
        {
            IRepository<Import> repository = new ImportRepository();
            var imports = repository.FindAll().Where(i => i.ImportedProduct.Id == productId);
            List<ImportDTO> importDTOs = new List<ImportDTO>();

            foreach (var i in imports)
            {
                importDTOs.Add(new ImportDTO(i.Id, i.PriceByUnit, i.Ammount, i.EntryDate, i.DepartureDate, i.ImportedProduct, i.ImporterClient, i.IsStored));
            }

            return importDTOs;
        }

        public IEnumerable<ImportDTO> FindImportsByName(string productName)
        {
            IRepository<Import> repository = new ImportRepository();
            var imports = repository.FindAll().Where(i => i.ImportedProduct.Name == productName);
            List<ImportDTO> importDTOs = new List<ImportDTO>();

            foreach (var i in imports)
            {
                importDTOs.Add(new ImportDTO(i.Id, i.PriceByUnit, i.Ammount, i.EntryDate, i.DepartureDate, i.ImportedProduct, i.ImporterClient, i.IsStored));
            }

            return importDTOs;
        }

        public IEnumerable<ImportDTO> FindImportByDate()
        {
            IRepository<Import> repository = new ImportRepository();
            var imports = repository.FindAll().Where(i => DateTime.Compare(i.DepartureDate,DateTime.Today) > 0);
            List<ImportDTO> importDTOs = new List<ImportDTO>();

            foreach (var i in imports)
            {
                importDTOs.Add(new ImportDTO(i.Id, i.PriceByUnit, i.Ammount, i.EntryDate, i.DepartureDate, i.ImportedProduct, i.ImporterClient, i.IsStored));
            }

            return importDTOs;
        }

        public IEnumerable<ImportDTO> GetImports()
        {
            IRepository<Import> repository = new ImportRepository();
            var imports = repository.FindAll();
            List<ImportDTO> importsDTO = new List<ImportDTO>();

            foreach(var i in imports)
            {
                importsDTO.Add(new ImportDTO(i.Id,i.PriceByUnit,i.Ammount,i.EntryDate,i.DepartureDate,i.ImportedProduct,i.ImporterClient,i.IsStored));
            }

            return importsDTO;
        }

        public bool TakeImport(int Id)
        {
            IRepository<Import> repository = new ImportRepository();
            Import import = repository.FindById(Id);
            import.IsStored = false;

            return repository.Update(import);
        }
    }
}
