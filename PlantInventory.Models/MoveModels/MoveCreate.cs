using PlantInventory.Data;
using PlantInventory.Models.BatchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantInventory.Models.MoveModels
{
    public class MoveCreate : BatchDetail
    {
        public location MoveFrom { get; set; }
        public location MoveTo { get; set; }
        public int NumberOfPotsMoved { get; set; }
        public string Comment { get; set; }
        public DateTimeOffset DateMoved { get; set; }
        


    }
}
