using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using Data.Models;
using Services.Interfaces;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        List<Customers> customers = new List<Customers>()
        {
            new Customers()
            {
                Id = "1325534F-9980-40B7-9573-A64E6F5071A6",
                FirstName = "Juan",
                LastName = "Dela Cruz"
            },
            new Customers()
            {
                Id = "3EE514A5-E28E-4BED-BC68-3F2A0087662C",
                FirstName="Anna",
                LastName="Santos"
            },
            new Customers()
            {
                Id = "6AD0E4C5-5527-4E66-AA10-123EE699FDC9",
                FirstName="Mario",
                LastName="Dimagiba"
            },
            new Customers()
            {
                Id = "DF0AF3D2-623D-4290-AF0C-EDC9EB221610",
                FirstName="Kristine",
                LastName="Reyes"
            },
            new Customers()
            {
                Id = "D4B9876E-639E-44BF-AE76-65BDEAC417D8",
                FirstName="Axel",
                LastName="Castores"
            }
        };
        public List<Customers> Customers(Func<Customers, bool> filter = null)
        {
            if (filter == null)
                return this.customers;
            return this.customers?.Where(filter).ToList();
        }

        public Customers Find(Func<Customers, bool> filter = null)
        {
            if (filter == null)
                return customers?.FirstOrDefault();
            return customers?.FirstOrDefault(filter);
        }
    }
}