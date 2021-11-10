using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.BatchModels
{
    public class BatchEdit
    {
        
        [Display(Name = "Herb")]
        public int HerbId { get; set; }
        public int BatchId { get; set; }
        public int TotalPotCount { get; set; }
        public DateTimeOffset ModifiedUTC { get; set; }
        public bool IsArchived { get; set; }
        public string ArchiveComment { get; set; }


    }
}
