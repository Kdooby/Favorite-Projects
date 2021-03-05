using AnotherInsuranceCompany.Models;
using System;
using System.Web.Mvc;

namespace AnotherInsuranceCompany.Controllers
{
    public class QuoteController : Controller
    
    {
        // GET: Quote
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Apply(string firstName, string lastName, string emailAddress,
                             DateTime dateOfBirth, int carYear, string carMake, string carModel,
                             string dui, int tickets, string coverage, string total)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) ||
                string.IsNullOrEmpty(dateOfBirth.ToString()) || string.IsNullOrEmpty(carYear.ToString()) || string.IsNullOrEmpty(carMake) ||
                string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(dui) || string.IsNullOrEmpty(tickets.ToString()) || string.IsNullOrEmpty(coverage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (AnotherInsuranceEntities db = new AnotherInsuranceEntities())
                {
                    var user = new Applicant();
                    user.FirstName = firstName;
                    user.LastName = lastName;
                    user.EmailAddress = emailAddress;
                    user.DateOfBirth = dateOfBirth;
                    user.CarYear = carYear;
                    user.CarMake = carMake;
                    user.CarModel = carModel;
                    user.DUI = dui;
                    user.SpeedingTickets = tickets;
                    user.CoverageType = coverage;
                    user.Total = quoteTotal(dateOfBirth,
                                       carYear,
                                       carMake,
                                       carModel,
                                       dui,
                                       tickets,
                                       coverage);

                    db.Applicants.Add(user);
                    db.SaveChanges();

                    ViewData["user.Total"] = user.Total;
                    return View("Index");
                }
            }
        }


        public int quoteTotal(DateTime dateOfBirth, int carYear, string carMake, string carModel,
                                  string dui, int tickets, string coverage)
        {
            using (AnotherInsuranceEntities data = new AnotherInsuranceEntities())
            {
                int totalQuote = 50;

                // Quote based on Age
                totalQuote = (DateTime.Now.Year - (Convert.ToDateTime(dateOfBirth)).Year < 18) ? totalQuote + 100 : totalQuote;
                totalQuote = (DateTime.Now.Year - (Convert.ToDateTime(dateOfBirth)).Year <= 25) ? totalQuote + 50 : totalQuote;
                totalQuote = (DateTime.Now.Year - (Convert.ToDateTime(dateOfBirth)).Year > 25) ? totalQuote + 25 : totalQuote;
                
                // Quote based on Car
                totalQuote = (Convert.ToInt32(carYear) < 2000) ? totalQuote + 25 : totalQuote;
                totalQuote = (Convert.ToInt32(carYear) > 2015) ? totalQuote + 25 : totalQuote;
                totalQuote = (carMake.ToLower() == "porsche") ? totalQuote + 25 : totalQuote;
                totalQuote = (carModel.ToLower() == "911 carrera") ? totalQuote + 25 : totalQuote;

                // Tickets and DUI's
                totalQuote = totalQuote + (Convert.ToInt32(tickets) * 10);
                totalQuote = (dui.ToLower() == "yes") ? (totalQuote + (totalQuote / 4)) : totalQuote;

                // Coverage
                totalQuote = (coverage.ToLower() == "full") ? (totalQuote + (totalQuote / 2)) : totalQuote;

                return Convert.ToInt32(totalQuote);
            }
        }
    }
}

