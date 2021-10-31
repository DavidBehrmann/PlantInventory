using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Data
{
    public class Batch
    {
        [Key]
        public int BatchId { get; set; }
        [ForeignKey("Herb")]
        public int HerbID { get; set; }
        public virtual Herb Herb { get; set; }
        [Required]
        public int TotalPotCount { get; set; }
        [Required]
        public DateTimeOffset DateReceived { get; set; }
    }
}
