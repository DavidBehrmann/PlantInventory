using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.BatchModels
{
    public class BatchDetail
    {
        public int BatchId { get; set; }
        [Required]
        public int TotalPotCount { get; set; }
        [Required]
        public DateTimeOffset DateReceived { get; set; }
    }
}
