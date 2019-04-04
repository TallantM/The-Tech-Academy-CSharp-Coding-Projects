using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsuranceQuotes.Models;

namespace InsuranceQuotes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear, string carMake, string carModel, bool dui, int numberOfTickets, bool fullCoverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(dateOfBirth.ToString()) || string.IsNullOrEmpty(carYear.ToString()) || string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(dui.ToString()) || string.IsNullOrEmpty(numberOfTickets.ToString()) || string.IsNullOrEmpty(fullCoverage.ToString()))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                int quote = 50;

                if ((DateTime.Now.Year - dateOfBirth.Year) < 25)
                {
                    quote += 25;
                }
                else if ((DateTime.Now.Year - dateOfBirth.Year) < 18)
                {
                    quote += 100;

                }
                else if ((DateTime.Now.Year - dateOfBirth.Year) > 100)
                {
                    quote += 25;

                }

                if (carYear < 2000)
                {
                    quote += 25;
                }
                else if (carYear > 2015)
                {
                    quote += 25;
                }

                if (carMake.ToLower() == "porsche")
                {
                    quote += 25;
                }

                if (carModel.ToLower() == "911 carrera")
                {
                    quote += 25;
                }

                for (int i = 0; i < numberOfTickets; i++)
                {
                    quote += 10;
                }

                if (dui)
                {
                    quote = (quote/4) + quote;
                }

                if (fullCoverage)
                {
                    quote = (quote / 2) + quote;
                }

                using (InsuranceQuotesDbEntities db = new InsuranceQuotesDbEntities())
                {
                    var newQuote = new Driver();
                    newQuote.FirstName = firstName;
                    newQuote.LastName = lastName;
                    newQuote.EmailAddress = emailAddress;
                    newQuote.DateOfBirth = dateOfBirth;
                    newQuote.CarYear = carYear;
                    newQuote.CarMake = carMake;
                    newQuote.CarModel = carModel;
                    newQuote.DUI = dui;
                    newQuote.NumberOfTickets = numberOfTickets;
                    newQuote.FullCoverage = fullCoverage;
                    newQuote.Quote = quote;

                    db.Drivers.Add(newQuote);
                    db.SaveChanges();
                    return View("Success", newQuote);
                }
            }
        }
    }
}