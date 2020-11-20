using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Domain.Interfaces
{
    public interface ICheckInOutRepository
    {
        int CheckInItem();
        int CheckOutItem();
        IList<int> CheckOutByStructure();
    }
}
