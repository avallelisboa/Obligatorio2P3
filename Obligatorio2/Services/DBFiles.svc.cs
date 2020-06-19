using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Obligatorio2.Models.BL;

namespace Obligatorio2.Services
{
    public class DBFiles : IDBFiles
    {
        public string ExportDatabase()
        {
            if (Utilities.MakeFiles()) return "Los archivos fueron creados correctamente";
            else return "Los archivos no han podido ser creados";
        }

        public string MakeTables()
        {
            if (Utilities.MakeTables()) return "La base de datos fue creada correctamente";
            else return "La base de datos no ha podido ser creada";
        }
    }
}
