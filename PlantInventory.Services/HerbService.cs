using PlantInventory.Data;
using PlantInventory.Models.HerbModels;
using PlantInventory.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Services
{
    public class HerbService
    {
        private readonly Guid _userID;

        public HerbService(Guid userID)
        {
            _userID = userID;
        }

        //Create Herb
        public bool CreateHerb(HerbCreate model)
        {
            var entity = new Herb()
            {
                UserId = _userID,
                HerbName = model.HerbName,
                IsArchived = false
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Herbs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //Get All Herb Inventory
        public IEnumerable<HerbDetail> GetAllHerbInventory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Herbs.Where(e => e.UserId == _userID && e.IsArchived == false).Select(
                    e => new HerbDetail
                    {
                        HerbId = e.HerbId,
                        HerbName = e.HerbName
                    });
                return query.ToArray();
            }
        }
        //Get All Archived Herb Inventory
        public IEnumerable<HerbDetail> GetAllArchivedHerbInventory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Herbs.Where(e => e.UserId == _userID && e.IsArchived == true).Select(
                    e => new HerbDetail
                    {
                        HerbId = e.HerbId,
                        HerbName = e.HerbName,
                        ArchiveComment = e.ArchiveComment
                    });
                return query.ToArray();
            }
        }


        //Get Herb by ID
        public HerbDetail GetHerbByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Herbs.Single(e => e.HerbId == id);
                return new HerbDetail
                {
                    HerbId = entity.HerbId,
                    HerbName = entity.HerbName,
                    ArchiveComment = entity.ArchiveComment,
                    IsArchived = entity.IsArchived
                };
            }
        }

        //Get Herb Name
        public string GetHerbName(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Herbs.First(e => e.HerbId == id);
                return entity.HerbName;
                
            }
        }

        //Edit Herb
        public bool EditHerb(HerbEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Herbs.Single(e => e.HerbId == model.HerbId);

                entity.HerbName = model.HerbName;
                entity.IsArchived = model.IsArchived;
                entity.ArchiveComment = model.ArchiveComment;

                return ctx.SaveChanges() == 1;
            }
        }


    }
}
