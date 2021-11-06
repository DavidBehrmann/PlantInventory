using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.StageModels
{
    public class StageCreate
    {
        public StageCreate()
        {
        }

        public StageCreate(int batchId, int countGrowRoom, int countPacking, int countFreshCut, int countDump)
        {
            BatchId = batchId;
            CountGrowRoom = countGrowRoom;
            CountPacking = countPacking;
            CountFreshCut = countFreshCut;
            CountDump = countDump;
        }

        public int BatchId { get; set; }
        public int CountGrowRoom { get; set; }
        public int CountPacking { get; set; }
        public int CountFreshCut { get; set; }
        public int CountDump { get; set; }
    }
}
