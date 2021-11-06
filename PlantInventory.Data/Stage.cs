using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Data
{
    public class Stage
    {
        [Key]
        public int StageId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [ForeignKey("Batch")]
        public int BatchId { get; set; }
        public virtual Batch Batch { get; set; }
        public int CountGrowRoom { get; set; }
        public int CountPacking { get; set; }
        public int CountFreshCut { get; set; }
        public int CountDump { get; set; }


    }
}
