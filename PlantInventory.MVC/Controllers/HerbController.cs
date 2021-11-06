using Microsoft.AspNet.Identity;
using PlantInventory.Models.HerbModels;
using PlantInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantInventory.MVC.Controllers
{
    [Authorize]
    public class HerbController : Controller
    {
        // GET: Herb
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new HerbService(userId);
            var model = service.GetAllHerbInventory();

            return View(model);
        }

        //GET: Herb
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HerbCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateHerbService();

            if (service.CreateHerb(model))
            {
                TempData["SaveResult"] = $"Herb {model.HerbName} has been created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", $"Herb {model.HerbName} could not be created.");
            return View(model);
        }

        private HerbService CreateHerbService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new HerbService(userId);
            return service;
        }

        public ActionResult Details(int id)
        {
            
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new HerbService(userId);
            var model = service.GetHerbByID(id);

            return View(model);
            
        }


    }
}