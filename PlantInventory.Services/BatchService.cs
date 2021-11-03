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
                HerbID = model.HerbID,
                TotalPotCount = model.TotalPotCount,
                DateReceived = model.DateReceived
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Batches.Add(entity);
                return ctx.SaveChanges() == 1;
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
                    DateReceived = entity.DateReceived
                };
            }
        }

        
        
        //Edit Batch


    }
}
