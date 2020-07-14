using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Obligatorio2.Services;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("imports")]
    public class ImportsController : ApiController
    {
        // GET: api/Imports
        [Route("getImports")]
        [HttpGet]
        public IHttpActionResult GetImports()
        {
            ImportServices proxy = new ImportServices();
            var imports = proxy.GetImports();
            return Ok(imports);
        }
        [Route("getImportsByProductId/{productId}")]
        [HttpGet]
        public IHttpActionResult GetImportsByProductId(string productId)
        {
            ImportServices proxy = new ImportServices();
            var imports = proxy.FindImportsById(productId);
            return Ok(imports);
        }
        [Route("getImportsByProductName/{productName}")]
        [HttpGet]
        public IHttpActionResult GetImportsByName(string productName)
        {
            ImportServices proxy = new ImportServices();
            var imports = proxy.FindImportsByName(productName);
            return Ok(imports);
        }
        [Route("getImportsByDate")]
        [HttpGet]
        public IHttpActionResult GetImportsByDate()
        {
            ImportServices proxy = new ImportServices();
            var imports = proxy.FindImportByDate();
            return Ok(imports);
        }

        [HttpPost]
        public IHttpActionResult TakeImport(string numberPlate, string address)
        {
            throw new NotImplementedException();
        }
    }
}
