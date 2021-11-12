using Microsoft.AspNet.Identity;
using PlantInventory.Models.StageModels;
using PlantInventory.Services;
using PlantInventory.Data;
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
        /*public ActionResult UpdateLocationCountsAfterMoveIsCreated(int id)
        {
            var stageService = CreateStageService();
            var update = stageService.GetStageByID(id);
            var model = new StageUpdateAfterMove
            {
                CountGrowRoom = update.CountGrowRoom,
                CountFreshCut = update.CountFreshCut,
                CountDump = update.CountDump,
                CountPacking = update.CountPacking
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateLocationCountsAfterMoveIsCreated(int id, StageUpdateAfterMove model)
        {
            if (!ModelState.IsValid) return View(model);

            var stageService = CreateStageService();
            var moveService = CreateMoveService();

            if (model.StageId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var stageBatchId = stageService.GetStageByBatchID(model.BatchId).BatchId;
            var moveBatchId = moveService.GetMoveByBatchID(model.BatchId).BatchId;
            
            if (stageBatchId == moveBatchId)
            {
                switch (moveService.GetMoveByBatchID(model.BatchId).MoveTo)
                {
                    case location.growRoom: moveService.MoveToGrowRoom();
                }

                if (stageService.UpdateStageAfterNewMoveCreated(model))
                {
                    TempData["SaveResult"] = "The Stage has been updated.";
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "There was an error in updating the stages.");
            return View(model);
        }*/


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
        private MoveService CreateMoveService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MoveService(userId);
            return service;
        }
    }
}