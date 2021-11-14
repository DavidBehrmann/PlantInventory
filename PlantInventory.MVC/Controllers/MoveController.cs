using Microsoft.AspNet.Identity;
using PlantInventory.Data;
using PlantInventory.Models.MoveModels;
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
    public class MoveController : Controller
    {
        private ApplicationDbContext _ctx = new ApplicationDbContext();

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


        public ActionResult Create()
        {
            List<SelectListItem> herbNames = new List<SelectListItem>();
            MoveCreate dateReceived = new MoveCreate();

            List<Herb> herbs = _ctx.Herbs.ToList();
            herbs.ForEach(h =>
            {
                herbNames.Add(new SelectListItem
                {
                    Text = h.HerbName,
                    Value = h.HerbId.ToString()
                });
                dateReceived.BatchDetails.DateReceive = herbNames;
                return View(herbNames);



            })
        }


        /*public ActionResult Create()
        {
            ViewBag.HerbName = _ctx.Herbs.Select(herb => new SelectListItem
            {
                Text = herb.HerbName,
                Value = herb.HerbId.ToString()
            }).ToArray();

            ViewBag.DateReceived = _ctx.Batches.Select(batch => new SelectListItem
            {
                Text = batch.DateReceived.Month + "/" + batch.DateReceived.Day,
                Value = batch.BatchId.ToString()
            });
            return View(new MoveCreate());
        }*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MoveCreate model)
        {
            ViewBag.HerbName = _ctx.Herbs.Select(herb => new SelectListItem
            {
                Text = herb.HerbName,
                Value = herb.HerbId.ToString()
            }).ToArray();
            ViewBag.DateReceived = _ctx.Batches.Select(batch => new SelectListItem
            {
                Text = batch.DateReceived.Month + "/" + batch.DateReceived.Day,
                Value = batch.BatchId.ToString()
            });
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateMoveService();


            if (service.CreateMove(model))
            {
                var herbService = CreateHerbService();
                var batchService = CreateBatchService();
                var batchDetail = batchService.GetBatchByID(model.BatchId);
                var herbName = herbService.GetHerbName(batchDetail.HerbId);


                switch (model.MoveTo)
                {
                    case location.growRoom:
                        service.MoveToGrowRoom(model);
                        break;
                    case location.freshCut:
                        service.MoveToFreshCut(model);
                        break;
                    case location.packing:
                        service.MoveToPacking(model);
                        break;
                    case location.dump:
                        service.MoveToDump(model);
                        break;
                }

                TempData["SaveResult"] = $"You have moved {model.NumberOfPotsMoved} pots of {herbName} from {model.MoveFrom} to {model.MoveTo}.";
                return RedirectToAction("Index", model.BatchId);
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
                return RedirectToAction("Index", model.BatchId);
            }

            ModelState.AddModelError("", "We were unable to update the move. Please ensure your data is correct.");
            return View(model);
        }

      
    
    }
}