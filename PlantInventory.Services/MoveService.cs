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
                DateMoved = DateTimeOffset.Now
                
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Moves.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //Get Move List
        public IEnumerable<MoveDetail> GetAllMoves()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Moves.Where(e => e.UserId == _userID).Select(
                    e => new MoveDetail
                    {
                        MoveId = e.MoveId,
                        BatchId = e.BatchId,
                        MoveFrom = e.MoveFrom,
                        MoveTo = e.MoveTo,
                        NumberOfPotsMoved = e.NumberOfPotsMoved,
                        DateMoved = e.DateMoved
                    });
                return query.ToArray();
            }
        }

        //Get Move Detail

        //Update Move



    }
}
