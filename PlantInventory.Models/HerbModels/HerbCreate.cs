using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.HerbModels
{
    public class HerbCreate
    {
        
        [Required]
        [Display(Name = "Herb Name")]

        public string HerbName { get; set; }
    }
}
