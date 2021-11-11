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
                var herbService = CreateHerbService();
                var batchService = CreateBatchService();
                var herbId = batchService.GetBatchByID(model.BatchId).HerbId;
                var herbName = herbService.GetHerbName(herbId);

                TempData["SaveResult"] = $"You have moved {model.NumberOfPotsMoved} pots of {herbName} from {model.MoveFrom} to {model.MoveTo}.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "This move has NOT been recorded. Please ensure your data is correct.");
            return View(model);
        }
        public ActionResult GetArchive(int batchId)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MoveService(userId);
            var model = service.GetAllMovesForABatchArchived(batchId);

            return View(model);
        }

        public ActionResult GetMoveByID(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MoveService(userId);
            var model = service.GetMoveByID(id);

            return View(model);
        }

        public ActionResult EditMove(int id)
        {
            var service = CreateMoveService();
            var edit = service.GetMoveByID(id);
            var model = new MoveEdit
            {
                MoveFrom = edit.MoveFrom,
                MoveTo = edit.MoveTo,
                NumberOfPotsMoved = edit.NumberOfPotsMoved,
                Comment = edit.Comment,
                ModifiedUTC = DateTimeOffset.Now,
                IsArchived = edit.IsArchived,
                ArchiveComment = edit.ArchiveComment
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMove(int id, MoveEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.MoveId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateMoveService();

            if (service.EditMove(model))
            {
                TempData["SaveResult"] = "You have updated the move.";
                return RedirectToAction("Index"); //could give me problems without a specified ID for the index
            }

            ModelState.AddModelError("", "We were unable to update the move. Please ensure your data is correct.");
            return View(model);
        }   
        
    }
}