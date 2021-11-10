using Microsoft.AspNet.Identity;
using PlantInventory.Models.MoveModels;
using PlantInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantInventory.MVC.Controllers
{
    [Authorize]
    public class MoveController : Controller
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
        private MoveService CreateMoveService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MoveService(userId);
            return service;
        }

        // GET: Move
        public ActionResult Index(int batchId)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MoveService(userId);
            var model = service.GetAllMovesForABatchNotArchived(batchId);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MoveCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateMoveService();


            if (service.CreateMove(model))
            {
                var batchService = CreateBatchService();
                var herbId = batchService.GetBatchByID(model.BatchId).HerbId;
                var herbName = herbId;

                TempData["SaveResult"] = $"You have moved {model.NumberOfPotsMoved} pots of {herbName} from {model.MoveFrom} to {model.MoveTo}.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "This move has NOT been recorded. Please ensure your data is correct.");
            return View(model);
        }
    }
}