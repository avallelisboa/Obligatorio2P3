using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Obligatorio2.Models.BL;
using Obligatorio2.Services;

namespace Obligatorio2.Controllers
{
    public class DepositController : Controller
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        Uri uri = null;

        public DepositController()
        {
            client.BaseAddress = new Uri("http://localhost:56488/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Convert.ToString(Session["Role"]) == "deposito")
            {
                ProductServices proxy = new ProductServices();
                ViewBag.productsList = proxy.GetProducts();

                return View("Deposit");
            }
            else return Redirect("../Home/Index");
        }
        [HttpGet]
        public ActionResult Clients()
        {
            if (Convert.ToString(Session["Role"]) == "deposito")
            {
                ClientServices proxy = new ClientServices();
                ViewBag.clientsList = proxy.GetClients();

                return View("Clients");
            }
            else return Redirect("../Home/Index");
        }
        [HttpPost]
        public ActionResult AddImport(string productId, long tin, int priceByUnit, int ammount, DateTime entryDate, DateTime departureDate, bool isStored)
        {
            ImportServices proxy = new ImportServices();
            if (proxy.AddImport(productId, tin, priceByUnit, ammount, isStored, entryDate, departureDate))
            {
                ViewBag.ImportAdded = true;
            }
            else
            {
                ViewBag.ImportAdded = false;
            }
            return Redirect("Clients");
        }

        [HttpGet]
        public ActionResult GetImports()
        {
            if (Convert.ToString(Session["Role"]) == "deposito") return View("Imports");
            else return Redirect("../Home/Index");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("../Home/Index");
        }
    }
}