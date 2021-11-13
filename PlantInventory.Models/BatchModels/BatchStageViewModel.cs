using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.BatchModels
{
    public class BatchStageViewModel
    {
        public IEnumerable<BatchDetail> BatchDetails { get; set; }
        public IEnumerable<StageModels.StageDetail> StageDetails { get; set; }

    }
}
