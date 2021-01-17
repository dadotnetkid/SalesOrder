using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;

namespace Services.Interfaces
{
    public interface ISalesOrderDetailService
    {

        public SalesOrderDetails Find(Func<SalesOrderDetails, bool> filter=null);
        
        public List<SalesOrderDetails> SalesOrderDetails(Func<SalesOrderDetails, bool> filter = null, string includeProperties = null);
        public SalesOrderDetails Insert(SalesOrderDetails model);
        public SalesOrderDetails Update(SalesOrderDetails model);


    }
}
