using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Data.Models;

namespace Services.VM
{
    public class TransformationMapsVM
    {
        private TransformationMaps _transformationMap;
        public int? TransformationMapId{ get; set; }
        public string SourceProductId { get; set; }
        public string DestinationProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ToleranceAmount { get; set; }
        public bool IsPercent { get; set; }


        public TransformationMaps TransformationMap
        {
            get
            {
                if (_transformationMap == null)
                    _transformationMap = new TransformationMaps();
                    return _transformationMap;
            }
            set => _transformationMap = value;
        }

        public List<TransformationMaps> TransformationMaps { get; set; }
        public List<Products> Products { get; set; }
    }
}
