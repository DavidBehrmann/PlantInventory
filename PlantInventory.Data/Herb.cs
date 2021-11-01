using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Data
{
    public class Herb
    {
        [Key]
        public int HerbId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [Display(Name = "Herb Name")]
        public string HerbName { get; set; }
    }
}
