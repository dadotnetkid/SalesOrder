using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Repository;
using Services.VM;

namespace Services
{
    public class TransformMapService : ITransformMapService
    {
        private UnitOfWork unitOfWork;
        private IProductService productService;

        public TransformMapService(UnitOfWork unitOfWork, IProductService productService)
        {
            this.unitOfWork = unitOfWork;
            this.productService = productService;
        }
        public List<TransformationMaps> Get()
        {
            return unitOfWork.TransformationMapsRepo.Get().Select(x => new TransformationMaps()
            {
                DestinationProductId = x.DestinationProductId,
                SourceProductId = x.SourceProductId,
                DestinationProducts = productService.Find(m => m.Id == x.DestinationProductId),
                SourceProducts = productService.Find(m => m.Id == x.SourceProductId),
                Id = x.Id,
                IsPercent = x.IsPercent,
                Quantity = x.Quantity,
                ToleranceAmount = x.ToleranceAmount
            }).ToList();
        }

        public List<Products> GetProducts()
        {
            return productService.Products();
        }

        public Results AddUpdate(TransformationMapsVM model)
        {
            try
            {

                if (model.TransformationMapId == null)
                {
                    unitOfWork.TransformationMapsRepo.Insert(model.TransformationMap);
                }
                else
                {
                    unitOfWork.TransformationMapsRepo.Update(model.TransformationMap);
                }

                unitOfWork.TransformationMapsRepo.SaveChanges();
                return new Results()
                {
                    Succeeded = true
                };

            }
            catch (Exception e)
            {
                return new Results()
                {
                    Succeeded = false,
                    Errors = new List<string>() { e.Message }
                };
            }
        }

        [HttpPost]
        public Results DeleteTransformMap(int transformationMapId)
        {
            try
            {
                unitOfWork.TransformationMapsRepo.Delete(x => x.Id == transformationMapId);
                unitOfWork.TransformationMapsRepo.SaveChanges();
                return new Results()
                {
                    Succeeded = true
                };
            }
            catch (Exception e)
            {
                return new Results()
                {
                    Succeeded = false,
                    Errors = new List<string>() { e.Message }
                };
            }
        }

        public TransformationMaps Find(int? transformationId)
        {
            return unitOfWork.TransformationMapsRepo.Find(x => x.Id == transformationId);
        }
    }
}
