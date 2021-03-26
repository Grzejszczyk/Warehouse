using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Domain.Interfaces
{
    public interface ICheckInOutRepository
    {
        int CheckInItem(int idemId, int itemQty, string userName);
        int CheckOutItem(int idemId, int itemQty, string userName);
        int CheckOutByStructure(int structureId, string userName);
        IQueryable<CheckIn> GetCheckIns();
        IQueryable<CheckOut> GetCheckOuts();
    }
}
