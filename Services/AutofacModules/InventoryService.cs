using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Services.Interfaces;
using Services.Repository;

namespace Services.AutofacModules
{
    public class InventoryService : IInventoryService
    {
        private UnitOfWork unitOfWork;
        private IProductService productService;

        public InventoryService(UnitOfWork unitOfWork, IProductService productService)
        {
            this.unitOfWork = unitOfWork;
            this.productService = productService;
        }
        public void Transform()
        {

        }

        public List<Inventory> GetInventories()
        {
            return unitOfWork.InventoryRepo.Get(x => x.TransformedInventories.Any()).Select(x => new Inventory()
            {
                Id = x.Id,
                ParentId = x.ParentId,
                ProductId = x.ProductId,
                Products = productService.Find(m => m.Id == x.ProductId),
                QTY = x.QTY,
                UOM = x.UOM,
                TransformedInventories = unitOfWork.InventoryRepo.Get(m => m.ParentId == x.Id),
                TransformedInventory = unitOfWork.InventoryRepo.Find(m => m.ParentId == x.Id)
            }).ToList();
        }
    }
}
