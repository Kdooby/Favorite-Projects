using AnotherInsuranceCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnotherInsuranceCompany.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (var db = new AnotherInsuranceEntities())
            {

                var users = db.Applicants;
                var userList = new List<Applicant>();

                foreach (var user in users)
                {
                    var quote = new Applicant();
                    quote.FirstName = quote.FirstName;
                    quote.LastName = quote.LastName;
                    quote.EmailAddress = quote.EmailAddress;
                    quote.Total = quote.Total;
                    userList.Add(user);

                }
                return View(userList);
            }
        }
    }
}

