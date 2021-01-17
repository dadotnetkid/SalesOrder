using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Data.Models;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        List<Customers> Customers(Func<Customers, bool> filter = null);
        Customers Find(Func<Customers, bool> filter = null);
    }
}
