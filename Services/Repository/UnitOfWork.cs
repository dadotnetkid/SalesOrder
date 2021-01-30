using System;

using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Models;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Repository;


namespace Services.Repository
{
    public partial class UnitOfWork : IDisposable
    {
        private bool disposed = false;
        private ModelDb context;


        public UnitOfWork(ModelDb db,
            IGenericRepository<SalesOrderDetails> salesOrderDetailRepo,
            IGenericRepository<SalesOrders> salesOrderRepo,
            IGenericRepository<SalesOrderPayments> salesOrderPaymentRepo,
            IGenericRepository<Cheques> chequesRepo,
            IGenericRepository<Inventory> inventoryRepo,
            IGenericRepository<TransformationMaps> transformationMapsRepo
            )
        {
            this.context = db;
            this.SalesOrderDetailRepo = salesOrderDetailRepo;
            this.SalesOrderRepo = salesOrderRepo;
            this.SalesOrderPaymentRepo = salesOrderPaymentRepo;
            this.ChequesRepo = chequesRepo;
            this.InventoryRepo = inventoryRepo;
            this.TransformationMapsRepo = transformationMapsRepo;
        }

        public IGenericRepository<TransformationMaps> TransformationMapsRepo { get; set; }

        public IGenericRepository<Inventory> InventoryRepo { get; set; }

        public IGenericRepository<Cheques> ChequesRepo { get; set; }

        public IGenericRepository<SalesOrderPayments> SalesOrderPaymentRepo { get; set; }

        public IGenericRepository<SalesOrders> SalesOrderRepo { get; set; }

        public IGenericRepository<SalesOrderDetails> SalesOrderDetailRepo { get; set; }


        public void Save()
        {
            context.SaveChanges();

        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}