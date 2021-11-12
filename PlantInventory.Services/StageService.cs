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
        public bool CreateStage(int batchId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var batch = ctx.Batches.Single(e => e.BatchId == batchId);
            var entity = new Stage()
            {

                UserId = _userID,
                BatchId = batchId,
                CountGrowRoom = batch.TotalPotCount,
                CountPacking = 0,
                CountFreshCut = 0,
                CountDump = 0,
                IsArchived = false
            };
                ctx.Stages.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //Get Stage by ID (stage and batch are 1 to 1)
        public StageDetail GetStageByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Stages.Single(e => e.StageId == id);
                return new StageDetail
                {
                    BatchId = entity.BatchId,
                    CountGrowRoom = entity.CountGrowRoom,
                    CountPacking = entity.CountPacking,
                    CountFreshCut = entity.CountFreshCut,
                    CountDump = entity.CountDump,
                    IsArchived = entity.IsArchived,
                    ArchiveComment = entity.ArchiveComment
                };
            }
        }
        public StageDetail GetStageByBatchID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Stages.Single(e => e.BatchId == id);
                return new StageDetail
                {
                    BatchId = entity.BatchId,
                    CountGrowRoom = entity.CountGrowRoom,
                    CountPacking = entity.CountPacking,
                    CountFreshCut = entity.CountFreshCut,
                    CountDump = entity.CountDump,
                    IsArchived = entity.IsArchived,
                    ArchiveComment = entity.ArchiveComment
                };
            }
        }

       /* //Edit Stage 
        public bool EditStage(StageEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Stages.Single(e => e.StageId == model.StageId);

                entity.CountGrowRoom = model.CountGrowRoom;
                entity.CountPacking = model.CountPacking;
                entity.CountFreshCut = model.CountFreshCut;
                entity.CountDump = model.CountDump;
                entity.IsArchived = model.IsArchived;
                entity.ArchiveComment = model.ArchiveComment;

                return ctx.SaveChanges() == 1;
            }
        }

        //Update Stage from new Move
        public bool UpdateStageAfterNewMoveCreated(StageUpdateAfterMove model)
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
        }*/
           

    }
}
