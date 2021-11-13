using PlantInventory.Data;
using PlantInventory.Models.BatchModels;
using PlantInventory.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Services
{
    public class BatchService
    {
        private readonly Guid _userID;
        public BatchService(Guid userID)
        {
            _userID = userID;
        }

        //Create Batch
        public bool CreateBatch(BatchCreate model)
        {
            var entity = new Batch()
            {
                UserId = _userID,
                HerbId = model.HerbId,
                TotalPotCount = model.TotalPotCount,
                DateReceived = DateTimeOffset.UtcNow.DateTime,
                IsArchived = false
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Batches.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //Get batches by creat DateTime
        public int GetNewestCreatedBatch()
        {
            using (var ctx = new ApplicationDbContext())
            {
                /* var oneMinute = new TimeSpan(0, 1, 0);
                 var query = ctx.Batches.Where(e => e.DateReceived > DateTimeOffset.UtcNow.DateTime - oneMinute).OrderByDescending(e => e.DateReceived).Take(1);*/

                var query = ctx.Batches.Select(e => e.BatchId).Max();
                return query;
            }
        
        }

        //Get all Batches
        public IEnumerable<BatchDetail> GetBatches(int herbId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Batches.Where(e => e.HerbId == herbId && e.IsArchived == false).Select(
                    e => new BatchDetail
                    {
                        BatchId = e.BatchId,
                        TotalPotCount = e.TotalPotCount,
                        DateReceived = e.DateReceived
                    });
                return query.ToArray();
            }
        }
        //Get all Archived Batches
        public IEnumerable<BatchDetail> GetArchivedBatches(int herbId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Batches.Where(e => e.HerbId == herbId && e.IsArchived == true).Select(
                    e => new BatchDetail
                    {
                        BatchId = e.BatchId,
                        TotalPotCount = e.TotalPotCount,
                        DateReceived = e.DateReceived,
                        ArchiveComment = e.ArchiveComment
                    });
                return query.ToArray();
            }
        }
        //Get Batch by ID
        public BatchDetail GetBatchByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Batches.Single(e => e.BatchId == id);
                return new BatchDetail
                {
                    BatchId = entity.BatchId,
                    TotalPotCount = entity.TotalPotCount,
                    DateReceived = entity.DateReceived,
                    ArchiveComment = entity.ArchiveComment
                };
            }
        }

        
        
        //Edit Batch
        public bool EditBatch(BatchEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Batches.Single(e => e.BatchId == model.BatchId);

                entity.HerbId = model.HerbId;
                entity.TotalPotCount = model.TotalPotCount;
                entity.ModifiedUTC = DateTimeOffset.UtcNow.DateTime;
                entity.IsArchived = model.IsArchived;
                entity.ArchiveComment = model.ArchiveComment;

                return ctx.SaveChanges() == 1;
            }
        }

        

    }
}
