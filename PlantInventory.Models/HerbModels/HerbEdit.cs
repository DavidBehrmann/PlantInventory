using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.HerbModels
{
    public class HerbEdit
    {
        public int HerbId { get; set; }
        public string HerbName { get; set; }

        public bool IsArchived { get; set; }
        public string ArchiveComment { get; set; }


    }
}
