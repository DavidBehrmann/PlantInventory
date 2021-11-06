using PlantInventory.Data;
using PlantInventory.Models.StageModels;
using PlantInventory.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Services
{
    public class StageService
    {
        private readonly Guid _userID;
        public StageService(Guid userID)
        {
            _userID = userID;
        }
        //Create Stage
        public bool CreateStage(StageCreate model)
        {
            var entity = new Stage()
            {
                UserId = _userID,
                BatchId = model.BatchId,
                CountGrowRoom = model.CountGrowRoom,
                CountPacking = model.CountPacking,
                CountFreshCut = model.CountFreshCut,
                CountDump = model.CountDump
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Stages.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //Get Stage by ID (stage and batch are 1 to 1)
        public StageCreate GetStageByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Stages.Single(e => e.BatchId == id);
                return new StageCreate
                {
                    BatchId = entity.BatchId,
                    CountGrowRoom = entity.CountGrowRoom,
                    CountPacking = entity.CountPacking,
                    CountFreshCut = entity.CountFreshCut,
                    CountDump = entity.CountDump
                };
            }
        }

        //Update Stage 
        public bool UpdateStage(StageEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Stages.Single(e => e.StageId == model.StageId);

                entity.CountGrowRoom = model.CountGrowRoom;
                entity.CountPacking = model.CountPacking;
                entity.CountFreshCut = model.CountFreshCut;
                entity.CountDump = model.CountDump;

                return ctx.SaveChanges() == 1;
            }
        }
        //Archive Stage
        public bool ArchiveBatch(StageArchive model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Stages.Single(e => e.StageId == model.StageId);

                entity.IsArchived = model.IsArchived;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
