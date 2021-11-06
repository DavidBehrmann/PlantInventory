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
        public ActionResult Index()
        {
            return View();
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
            var service = CreateStageService();

            if (service.CreateStage(model))
            {

            }

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
    }
}