using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnotherInsuranceCompany.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Another Insurance";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Another Insurance Now!";

            return View();
        }

        public ActionResult Quote()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }
    }

}