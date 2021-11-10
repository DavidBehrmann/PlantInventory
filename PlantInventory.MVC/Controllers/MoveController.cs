using Microsoft.AspNet.Identity;
using PlantInventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantInventory.MVC.Controllers
{
    public class MoveController : Controller
    {
        [Authorize]
        // GET: Move
        public ActionResult Index(int batchId)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MoveService(userId);
            var model = service.GetAllMovesForABatchNotArchived(batchId);

            return View(model);
        }
    }
}