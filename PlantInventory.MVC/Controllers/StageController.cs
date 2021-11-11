using Microsoft.AspNet.Identity;
using PlantInventory.Models.StageModels;
using PlantInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantInventory.MVC.Controllers
{
    public class StageController : Controller
    {
        // GET: Stage
        public ActionResult Index(int batchId)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StageService(userId);
            var model = service.GetStageByID(batchId);

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StageCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var herbService = CreateHerbService();
            var batchService = CreateBatchService();
            var herbId = batchService.GetBatchByID(model.BatchId).HerbId;
            var herbName = herbService.GetHerbName(herbId);
            var dateReceived = batchService.GetBatchByID(model.BatchId).DateReceived;

            var service = CreateStageService();

            if (service.CreateStage(model))
            {
                TempData["SaveResult"] = $"The stages have been attached to this batch of {herbName}" +
                    $" received on {dateReceived}.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "There was an error creating the stage for this batch.");
            return View(model);

        }

        //Have stage update when Moves are created (stageEdit)


        //Archive method (to archive if batch gets archived)



        private StageService CreateStageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new StageService(userId);
            return service;
        }
        private BatchService CreateBatchService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BatchService(userId);
            return service;
        }
        private HerbService CreateHerbService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new HerbService(userId);
            return service;
        }
    }
}