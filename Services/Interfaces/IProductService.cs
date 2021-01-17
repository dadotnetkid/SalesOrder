using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;

namespace Services.Interfaces
{
    public interface IProductService
    {
        public Products Find(Func<Products, bool> filter=null);
        public List<Products> Products(Func<Products, bool> filter = null);
    }
}
