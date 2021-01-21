using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Services.Interfaces;

namespace Services.VM
{
    public class SalesOrderDetailVM
    {
        private IProductService productService;
        public SalesOrderDetailVM()
        {
            this.productService = new ProductService();
        }
        private SalesOrderDetails salesOrderDetails;
        public string SalesOrderId { get; set; }
        public string SalesOrderDetailId { get; set; }

        public SalesOrderDetails SalesOrderDetails
        {
            get
            {
                if (salesOrderDetails == null)
                    salesOrderDetails = new SalesOrderDetails();
                return salesOrderDetails;
            }
            set { salesOrderDetails = value; }
        }

        public List<Products> Products => productService.Products();
        public bool isSuccess{ get; set; }
    }
}
