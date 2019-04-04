using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InsuranceQuotes.Models;

namespace InsuranceQuotes.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            using (InsuranceQuotesDbEntities db = new InsuranceQuotesDbEntities())
            {
                var drivers = db.Drivers.ToList();
                var driverModels = new List<Driver>();
                foreach (var driver in drivers)
                {
                    var driverModel = new Driver();
                    driverModel.FirstName = driver.FirstName;
                    driverModel.LastName = driver.LastName;
                    driverModel.EmailAddress = driver.EmailAddress;
                    driverModel.Quote = driver.Quote;
                    driverModels.Add(driverModel);
                }
                return View(driverModels);
            }
        }
    }
}