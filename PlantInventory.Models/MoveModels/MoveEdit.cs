using PlantInventory.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.MoveModels
{
    public class MoveEdit
    {
        public int MoveId { get; set; }
        public int BatchId { get; set; }
        public location MoveFrom { get; set; }
        public location MoveTo { get; set; }
        public int NumberOfPotsMoved { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset DateMoved { get; set; }
        [Display(Name = "Date Modified")]
        public DateTimeOffset ModifiedUTC { get; set; }

        public bool IsArchived { get; set; }
        public string ArchiveComment { get; set; }

    }
}
