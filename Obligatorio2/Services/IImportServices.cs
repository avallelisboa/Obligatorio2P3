using System;
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
    }
}
