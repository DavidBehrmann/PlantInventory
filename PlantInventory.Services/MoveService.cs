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
        public IEnumerable<MoveDetail> GetAllMovesForABatchNotArchived(int batchId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Moves.Where(e => e.BatchId == batchId && e.IsArchived == false).Select(
                    e => new MoveDetail
                    {
                        MoveId = e.MoveId,
                        BatchId = e.BatchId,
                        MoveFrom = e.MoveFrom,
                        MoveTo = e.MoveTo,
                        NumberOfPotsMoved = e.NumberOfPotsMoved,
                        DateMoved = e.DateMoved,
                        Comment = e.Comment,
                        ModifiedUTC = e.ModifiedUTC
                    });
                return query.ToArray();
            }
        }
        public IEnumerable<MoveDetail> GetAllMovesForABatchArchived(int batchId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Moves.Where(e => e.BatchId == batchId && e.IsArchived == true).Select(
                    e => new MoveDetail
                    {
                        MoveId = e.MoveId,
                        BatchId = e.BatchId,
                        MoveFrom = e.MoveFrom,
                        MoveTo = e.MoveTo,
                        NumberOfPotsMoved = e.NumberOfPotsMoved,
                        DateMoved = e.DateMoved,
                        Comment = e.Comment,
                        ModifiedUTC = e.ModifiedUTC,
                        ArchiveComment = e.ArchiveComment

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
                    DateMoved = entity.DateMoved,
                    ModifiedUTC = entity.ModifiedUTC,
                    IsArchived = entity.IsArchived,
                    ArchiveComment = entity.ArchiveComment
                };
            }
        }

        //Edit Move
        public bool EditMove(MoveEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Moves.Single(e => e.MoveId == model.MoveId);

                entity.MoveFrom = model.MoveFrom;
                entity.MoveTo = model.MoveTo;
                entity.NumberOfPotsMoved = model.NumberOfPotsMoved;
                entity.Comment = model.Comment;
                entity.IsArchived = model.IsArchived;
                entity.ArchiveComment = model.ArchiveComment;
                entity.ModifiedUTC = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
