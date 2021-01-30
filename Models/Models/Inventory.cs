using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string UOM { get; set; }
        public int QTY { get; set; }
        public int ParentId { get; set; }
        public Inventory TransformedInventory { get; set; }
        public ICollection<Inventory> TransformedInventories { get; set; }
        [NotMapped]
        public Products Products { get; set; }
    }
}
