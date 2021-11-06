using PlantInventory.Data;
using PlantInventory.Models.MoveModels;
using PlantInventory.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Services
{
    public class MoveService
    {
        private readonly Guid _userID;

        public MoveService(Guid userID)
        {
            _userID = userID;
        }
        //Create Move
        public bool CreateMove(MoveCreate model)
        {
            var entity = new Move()
            {
                UserId = _userID,
                BatchId = model.BatchId,
                MoveFrom = model.MoveFrom,
                MoveTo = model.MoveTo,
                NumberOfPotsMoved = model.NumberOfPotsMoved,
                Comment = model.Comment,
                DateMoved = DateTimeOffset.Now,
                IsArchived = false
                
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Moves.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //Get Move List
        public IEnumerable<MoveDetail> GetAllMovesForABatch(int batchId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Moves.Where(e => e.BatchId == batchId).Select(
                    e => new MoveDetail
                    {
                        MoveId = e.MoveId,
                        MoveFrom = e.MoveFrom,
                        MoveTo = e.MoveTo,
                        NumberOfPotsMoved = e.NumberOfPotsMoved,
                        DateMoved = e.DateMoved
                    });
                return query.ToArray();
            }
        }

        //Get Move Detail
        public MoveDetail GetMoveByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Moves.Single(e => e.MoveId == id);
                return new MoveDetail
                {
                    MoveId = entity.MoveId,
                    BatchId = entity.BatchId,
                    MoveFrom = entity.MoveFrom,
                    MoveTo = entity.MoveTo,
                    Comment = entity.Comment,
                    NumberOfPotsMoved = entity.NumberOfPotsMoved,
                    DateMoved = entity.DateMoved
                };
            }
        }

        //Archive Move
        public bool ArchiveMove(MoveArchive model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Moves.Single(e => e.MoveId == model.MoveId);

                entity.IsArchived = model.IsArchived;

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
