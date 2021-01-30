using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Models
{
    [Table("TransformationMaps")]
    public class TransformationMaps
    {
        public int? Id { get; set; }
        [MaxLength(128)]
        public string SourceProductId { get; set; }
        [MaxLength(128)]
        public string DestinationProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ToleranceAmount { get; set; }
        public bool IsPercent { get; set; }
        [NotMapped]
        public Products SourceProducts { get; set; }
        [NotMapped]
        public Products DestinationProducts { get; set; }
    }

}
