using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;

namespace Services.Interfaces
{
    public interface IInventoryService
    {
        void Transform();
        List<Inventory> GetInventories();
    }
}
