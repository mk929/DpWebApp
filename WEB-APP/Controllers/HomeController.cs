using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DpWebApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Malaysia Visa Application Appointment Request.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Malaysia Visa Application (OSC)";

            return View();
        }
        public ActionResult Error()
        {
            ViewBag.Message = "An Error occured. Please contact the Administrator for assistant.";

            return View();
        }
    }
}