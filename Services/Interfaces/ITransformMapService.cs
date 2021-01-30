using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Services.Repository;
using Services.VM;

namespace Services.Interfaces
{
    public interface ITransformMapService
    {
        public List<TransformationMaps> Get();
        public TransformationMaps Find(int? transformationId);
        List<Products> GetProducts();
        Results AddUpdate(TransformationMapsVM model);
        Results DeleteTransformMap(int transformationMapId);
    }
}
