using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.BatchModels
{
    public class BatchCreate
    {
        [Required]
        [Display(Name = "Herb")]
        public int HerbID { get; set; }
        [Required]
        public int TotalPotCount { get; set; }
        [Required]
        public DateTimeOffset DateReceived { get; set; }
    }
}
