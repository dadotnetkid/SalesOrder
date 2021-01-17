using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Services.Interfaces;

namespace Services
{
    public class ProductService : IProductService
    {
        private List<Products> products = new List<Products>()
        {
            new Products()
            {
                Id="F486C6AE-1F56-4B32-AE10-CC65ABDA39EE",
                SKU="TSH-MED-WHI-COT",
                ProductName="T-shirt White Cotton",
                PurchaseAmount=350M,
                SellingAmount=360M
            },
            new Products()
            {
                Id="5F87E716-7BC0-4344-9420-CE8BB31284F5",
                SKU="BAG-BLA-BAG-SAM",
                ProductName="Samsonite Bagpack Black",
                PurchaseAmount=1553.99M,
                SellingAmount=1600.50M
            },
            new Products()
            {
                Id="F318F049-C54F-4371-B876-6B706B98A659",
                SKU="BAB-WHI-FRO",
                ProductName="Baby Shirt Frogsuit White",
                PurchaseAmount=356.65M,
                SellingAmount=370.65M
            },
            new Products()
            {
                Id="DEEB9FE3-E123-4B00-9390-DF2C2CB369A7",
                SKU="REF-PAN-INV-8FT",
                ProductName="Panasonic Refrigerator Inverter 8Ft",
                PurchaseAmount=15750.75M,
                SellingAmount=17500.50M
            },
            new Products()
            {
                Id="905F0889-013E-4A0E-B522-5E8F8911E698",
                SKU="BUT-NIK-ORI-JAP",
                ProductName="The Original Japanese Butcher’s knife NIKUYA",
                PurchaseAmount=1000.00M,
                SellingAmount=1200M
            }
        };
        public Products Find(Func<Products, bool> filter = null)
        {
            if (filter == null)
                return products.FirstOrDefault();
            return products.FirstOrDefault(filter);
        }

        public List<Products> Products(Func<Products, bool> filter = null)
        {
            if (filter == null)
                return products;
            return products.Where(filter).ToList();
        }
    }
}
