using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Data
{
    public enum location
    {
        growRoom,
        packing,
        freshCut,
        dump,
    }
    public class Move
    {
        
        [Key]
        public int MoveId { get; set; }
        [ForeignKey("Batch")]
        public int BatchId { get; set; }
        public virtual Batch Batch { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public location MoveFrom { get; set; }
        [Required]
        public location MoveTo { get; set; }
        [Required]
        public int NumberOfPotsMoved { get; set; }
        public string Comment { get; set; }
        [Required]
        public DateTimeOffset DateMoved { get; set; }

        public bool IsArchived { get; set; }

    }
}
