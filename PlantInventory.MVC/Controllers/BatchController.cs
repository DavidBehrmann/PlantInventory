using Microsoft.AspNet.Identity;
using PlantInventory.Models.BatchModels;
using PlantInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantInventory.MVC.Controllers
{
    [Authorize]
    public class BatchController : Controller
    {
        // GET: Batch
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BatchCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateBatchService();

            if (service.CreateBatch(model))
            {
                var herbService = CreateHerbService();

                var herbName = herbService.GetHerbName(model.HerbID);

                TempData["SaveResult"] = $"You have created a new batch of {herbName} received on {model.DateReceived.DayOfYear}.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The batch could not be created.");
            return View(model);
        }
        private HerbService CreateHerbService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new HerbService(userId);
            return service;
        }

        private BatchService CreateBatchService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BatchService(userId);
            return service;
        }

    }
}