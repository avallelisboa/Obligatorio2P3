using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Obligatorio2.Services;

namespace API.Controllers
{
    public class ImportsController : ApiController
    {
        // GET: api/Imports
        [HttpGet]
        public IHttpActionResult GetImports()
        {
            ImportServices proxy = new ImportServices();
            var imports = proxy.GetImports();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(imports);

            return Ok(json);
        }

        [HttpGet]
        public IHttpActionResult GetImportsByProductId(string productId)
        {
            ImportServices proxy = new ImportServices();
            var imports = proxy.FindImportsById(productId);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(imports);

            return Ok(json);
        }

        [HttpGet]
        public IHttpActionResult GetImportsByName(string productName)
        {
            ImportServices proxy = new ImportServices();
            var imports = proxy.FindImportsByName(productName);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(imports);

            return Ok(json);
        }

        [HttpGet]
        public IHttpActionResult GetImportsByDate()
        {
            ImportServices proxy = new ImportServices();
            var imports = proxy.FindImportByDate();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(imports);

            return Ok(json);
        }

        [HttpPost]
        public IHttpActionResult TakeImport(string numberPlate, string address)
        {
            throw new NotImplementedException();
        }
    }
}
