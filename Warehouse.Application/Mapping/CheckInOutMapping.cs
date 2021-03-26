using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.ViewModels.CheckInOut;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Mapping
{
    internal class CheckInOutMapping
    {
        internal List<CheckInOutForListVM> CheckInOutList(IQueryable<CheckIn> checkIns)
        {
            return checkIns.Select(checkIn => new CheckInOutForListVM
            {
                CheckInOutId = checkIn.Id,
                ItemId = checkIn.Item.Id,
                Quantity = checkIn.Quantity,
                CreatedById = checkIn.CreatedById,
                CreatedDateTime = checkIn.CreatedDateTime
            }).ToList();
        }
        internal List<CheckInOutForListVM> CheckInOutList(IQueryable<CheckOut> checkOuts)
        {
            return checkOuts.Select(checkOut => new CheckInOutForListVM
            {
                CheckInOutId = checkOut.Id,
                ItemId = checkOut.Item.Id,
                Quantity = checkOut.Quantity,
                CreatedById = checkOut.CreatedById,
                CreatedDateTime = checkOut.CreatedDateTime
            }).ToList();
        }
    }
}
