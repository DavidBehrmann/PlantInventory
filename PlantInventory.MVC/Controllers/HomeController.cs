using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantInventory.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "I made this to show that I could code well.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please reach out with any questions or concerns.";

            return View();
        }
    }
}