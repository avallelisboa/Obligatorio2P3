using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Obligatorio2.Services
{
    [ServiceContract]
    interface IClientServices
    {
        [OperationContract]
        IEnumerable<ClientDTO> GetClients();

        [OperationContract]
        int GetExpectedIncome(long tin);
    }
}
