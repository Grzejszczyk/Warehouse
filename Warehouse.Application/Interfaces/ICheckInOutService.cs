using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.ViewModels.CheckInOut;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Application.ViewModels.Structure;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Interfaces
{
    public interface ICheckInOutService
    {
        int CheckIn(CheckInOutVMForCreate checkInOutVMForCreate, string userName);
        int CheckOut(CheckInOutVMForCreate checkInOutVMForCreate, string userName);
        int CheckOutByStructure(int structureId, string userName);
        List<CheckInOutForListVM> GetCheckIns(int itemId);
        List<CheckInOutForListVM> GetCheckOuts(int itemId);
    }
}
