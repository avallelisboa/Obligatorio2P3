using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Obligatorio2.Services
{
    [ServiceContract]
    public interface IDBFiles
    {
        [OperationContract]
        string ExportDatabase();

        [OperationContract]
        string MakeTables();
    }
}