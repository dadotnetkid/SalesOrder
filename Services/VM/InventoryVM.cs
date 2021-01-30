using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;

namespace Services.VM
{
    public class InventoryVM
    {
        public int? InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
