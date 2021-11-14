using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.BatchModels
{
    public class BatchStageViewModel
    {
        public BatchDetail BatchDetails { get; set; }
        public StageModels.StageDetail StageDetails { get; set; }
        public HerbModels.HerbDetail HerbDetails { get; set; }

    }
}
