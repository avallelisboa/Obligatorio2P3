using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obligatorio2.Models.BL;
using Obligatorio2.Models.Repositories;
using Obligatorio2.Services;

namespace Obligatorio2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(int id, string password, string role)
        {
            Session["RegisterError"] = null;
            Session["LoginError"] = null;


            if(id <= 0 || password == null || role == null)
            {
                Session["RegisterError"] = "Debe completar todos los datos de forma correcta.";
                return Redirect("../Home/Index");
            }

            if(role == "admin")
            {
                Admin admin = new Admin(id, password);                
                var result = admin.isUserValid();

                bool isCorrect; string resultMessage;
                isCorrect = result.Item1;
                resultMessage = result.Item2;

                if (!isCorrect)
                {
                    Session["RegisterError"] = resultMessage;
                    return Redirect("../Home/Index");
                }
                else
                {
                    IRepository<User> userRepository = new UserRepository();
                    userRepository.Add(admin);

                    Session["RegisterError"] = resultMessage;
                    return Redirect("../Home/Index");
                }
            }

            else if(role == "deposito")
            {
                Deposit deposit = new Deposit(id, password);
                var result = deposit.isUserValid();

                bool isCorrect; string resultMessage;
                isCorrect = result.Item1;
                resultMessage = result.Item2;

                if (!isCorrect)
                {
                    Session["RegisterError"] = resultMessage;
                    return Redirect("../Home/Index");
                }
                else
                {
                    IRepository<User> userRepository = new UserRepository();
                    userRepository.Add(deposit);

                    Session["RegisterError"] = resultMessage;
                    return Redirect("../Home/Index");
                }
            }
            else
            {
                Session["RegisterError"] = "El rol ingresado no es válido";
                return Redirect("../Home/Index");
            }            
        }

        [HttpPost]
        public ActionResult Login(int id, string password)
        {
            Session.Clear();

            IRepository<User> userRepository = new UserRepository();
            User user = userRepository.FindById(id);

            if (user == null) Session["LoginError"] = "El usuario no existe";
            else if (!(user.Password == password)) Session["LoginError"] = "La contraseña no es correcta";
            else
            {
                Session["LoggedUser"] = user;
                Session["Role"] = user.Role;
                if (user.Role == "admin") return Redirect("../Admin/Index");
                if (user.Role == "deposito") return Redirect("../Deposit/Index");
            }
            return Redirect("../Home/Index");
        }

        public ActionResult ExportDB()
        {          
            DBFiles proxy = new DBFiles();
            ViewBag.DataMessage = proxy.ExportDatabase();

            return View("Index");
        }

        public ActionResult ImportDB()
        {
            DBFiles proxy = new DBFiles();
            ViewBag.DataMessage = proxy.MakeTables();

            return View("Index");
        }
    }
}