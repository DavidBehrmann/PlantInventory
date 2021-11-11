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
        // GET: Batch
        public ActionResult Index(int herbId)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BatchService(userId);
            var model = service.GetBatches(herbId);
            return View(model);
        }
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
                var herbName = herbService.GetHerbName(model.HerbId);
                

                TempData["SaveResult"] = $"You have created a new batch of {herbName} received on {model.DateReceived.DayOfYear}.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "The batch could not be created.");
            return View(model);
        }
        

        public ActionResult GetBatchByID(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BatchService(userId);
            var model = service.GetBatchByID(id);

            return View(model);
        }
        public ActionResult EditBatch(int id)
        {
            var service = CreateBatchService();
            var edit = service.GetBatchByID(id);
            var model = new BatchEdit
            {
                HerbId = edit.HerbId,
                TotalPotCount = edit.TotalPotCount,
                ModifiedUTC = DateTimeOffset.Now,
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
                return RedirectToAction("Index"); //could give me problems without a specified ID for the index
            }

            ModelState.AddModelError("", "We were unable to update the batch. Please ensure your data is correct.");
            return View(model);
        }
    }
}