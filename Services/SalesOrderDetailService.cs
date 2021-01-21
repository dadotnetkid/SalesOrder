using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services
{
    public class SalesOrderDetailService : ISalesOrderDetailService
    {
        private ModelDb db;
        private IProductService productService;

        public SalesOrderDetailService(ModelDb db, IProductService productService)
        {
            this.db = db;
            this.productService = productService;
        }
        public SalesOrderDetails Find(Func<SalesOrderDetails, bool> filter = null)
        {
            if (filter != null)
                return db.SalesOrderDetails.FirstOrDefault(filter);
            return db.SalesOrderDetails.FirstOrDefault();
        }

        public List<SalesOrderDetails> SalesOrderDetails(Func<SalesOrderDetails, bool> filter = null, string includeProperties = null)
        {
            var dbSet = db.SalesOrderDetails.AsQueryable();
            if (includeProperties != null)
            {
                dbSet = db.SalesOrderDetails.Include(includeProperties);
            }
            if (filter != null)
                return dbSet.Where(filter).ToList();
            return db.SalesOrderDetails.ToList();
        }

        public SalesOrderDetails Insert(SalesOrderDetails model)
        {
            var product = productService.Find(x => x.ProductName == model.ProductName);
            model.Sku = product.SKU;
            model.PurchaseAmount = product.PurchaseAmount;
            model.SellingPrice = product.SellingAmount;
            model.Id = Guid.NewGuid().ToString();
            db.SalesOrderDetails.Add(model);
            db.SaveChanges();
            return model;
        }

        public SalesOrderDetails Update(SalesOrderDetails model)
        {
            var item = db.SalesOrderDetails.Find(model.Id);
            var product = productService.Find(x => x.ProductName == model.ProductName);


            item.ProductName = product.ProductName;
            item.PurchaseAmount = product.PurchaseAmount;
            item.SellingPrice = product.SellingAmount;
            item.Sku = product.SKU;
            item.Qty = model.Qty;
            db.SaveChanges();
            return model;
        }

        public int Delete(Func<SalesOrderDetails, bool> filter = null)
        {
            try
            {
                db.SalesOrderDetails.Remove(db.SalesOrderDetails.FirstOrDefault(filter));
                return db.SaveChanges();

            }
            catch (Exception e)
            {

            }

            return 0;
        }
    }
}
