using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Application.Interfaces
{
    public interface ICheckInOutService
    {
        bool CheckIn();
        bool CheckOut();
        int CheckOutByStructure();
    }
}
