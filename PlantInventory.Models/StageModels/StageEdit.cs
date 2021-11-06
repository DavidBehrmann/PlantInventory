using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.StageModels
{
    public class StageEdit
    {
        public int StageId { get; set; }
        public int BatchId { get; set; }
        public int CountGrowRoom { get; set; }
        public int CountPacking { get; set; }
        public int CountFreshCut { get; set; }
        public int CountDump { get; set; }
    }
}
