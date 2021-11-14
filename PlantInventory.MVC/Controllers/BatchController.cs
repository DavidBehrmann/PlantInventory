using Microsoft.AspNet.Identity;
using PlantInventory.Data;
using PlantInventory.Models.BatchModels;
using PlantInventory.MVC.Models;
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
        private ApplicationDbContext _ctx = new ApplicationDbContext();
        private HerbService CreateHerbService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new HerbService(userId);
            return service;
        }
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
        // GET: Batch
        public ActionResult Index(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BatchService(userId);
            var model = service.GetBatches(id);
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.HerbName = _ctx.Herbs.Select(herb => new SelectListItem
            {
                Text = herb.HerbName,
                Value = herb.HerbId.ToString()
            }).ToArray();
            return View(new BatchCreate());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BatchCreate model)
        {
            ViewBag.HerbName = _ctx.Herbs.Select(herb => new SelectListItem
            {
                Text = herb.HerbName,
                Value = herb.HerbId.ToString()
            }).ToArray();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateBatchService();

            if (service.CreateBatch(model))
            {
                var herbService = CreateHerbService();
                var stageService = CreateStageService();
                var herbName = herbService.GetHerbName(model.HerbId);
                var newBatch = service.GetNewestCreatedBatch();
                stageService.CreateStage(newBatch);

                TempData["SaveResult"] = $"You have created a new batch of {herbName} received on {DateTimeOffset.UtcNow.Date}.";
                //var newmodel = service.GetBatches(model.HerbId);
                return RedirectToAction("Index", "Batch", new { @id = model.HerbId } );
            }
            ModelState.AddModelError("", "The batch could not be created.");

            return View(model);
        }
        

        public ActionResult GetBatchByID(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var batchService = new BatchService(userId);
            var stageService = new StageService(userId);
            var herbService = new HerbService(userId);
            BatchStageViewModel batchStageDetails = new BatchStageViewModel();
            batchStageDetails.BatchDetails = batchService.GetBatchByID(id);
            batchStageDetails.StageDetails = stageService.GetStageByBatchID(id);
            batchStageDetails.HerbDetails = herbService.GetHerbByID(batchService.GetBatchByID(id).HerbId);

            batchStageDetails.BatchDetails.BatchId = id;
            batchStageDetails.HerbDetails.HerbId = batchStageDetails.BatchDetails.HerbId;
            batchStageDetails.HerbDetails.HerbName = batchStageDetails.HerbDetails.HerbName;
            //I need this method to return the stage details as well so we can display the pot counts by location.

            return View(batchStageDetails);
        }
        public ActionResult EditBatch(int id)
        {
            var service = CreateBatchService();
            var edit = service.GetBatchByID(id);
            var model = new BatchEdit
            {
                HerbId = edit.HerbId,
                BatchId = edit.BatchId,
                TotalPotCount = edit.TotalPotCount,
                ModifiedUTC = DateTimeOffset.Now.Date,
                IsArchived = edit.IsArchived,
                ArchiveComment = edit.ArchiveComment
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBatch(int id, BatchEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BatchId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateBatchService();

            if (service.EditBatch(model))
            {
                TempData["SaveResult"] = "You have updated this Batch.";
                return RedirectToAction("Index", "Batch", new { @id = model.HerbId });
            }

            ModelState.AddModelError("", "We were unable to update the batch. Please ensure your data is correct.");
            return View(model);
        }

        public string GetHerbNameForBatchIndexView(int id)
        {
            var service = CreateHerbService();
            var herbName = service.GetHerbName(id);
            return herbName;
        }
        public string GetHerbNameForGetBatchByIDView(int herbId)
        {
            var service = CreateHerbService();
            var herbName = service.GetHerbName(herbId);
            return herbName;
        }
    }
}