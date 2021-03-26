using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Mapping;
using Warehouse.Application.ViewModels.CheckInOut;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Application.ViewModels.Pagination;
using Warehouse.Application.ViewModels.Structure;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Services
{
    public class CheckInOutService : ICheckInOutService
    {
        private readonly ICheckInOutRepository _checkInOutRepository;
        private CheckInOutMapping checkInOutMapping;
        public CheckInOutService(ICheckInOutRepository checkInOutRepo)
        {
            _checkInOutRepository = checkInOutRepo;
            checkInOutMapping = new CheckInOutMapping();
        }

        public int CheckIn(CheckInOutVMForCreate checkInOutVMForCreate, string userName)
        {
            _checkInOutRepository.CheckInItem(checkInOutVMForCreate.ItemId, checkInOutVMForCreate.Quantity, userName);
            return checkInOutVMForCreate.ItemId;
        }

        public int CheckOut(CheckInOutVMForCreate checkInOutVMForCreate, string userName)
        {
            _checkInOutRepository.CheckOutItem(checkInOutVMForCreate.ItemId, checkInOutVMForCreate.Quantity, userName);
            return checkInOutVMForCreate.ItemId;
        }

        public int CheckOutByStructure(int structureId, string userName)
        {
            _checkInOutRepository.CheckOutByStructure(structureId, userName);
            return structureId;
        }

        public List<CheckInOutForListVM> GetCheckIns(int itemId)
        {
            if (itemId == 0)
            {
                var checkIns = _checkInOutRepository.GetCheckIns();
                var checkInsList = checkInOutMapping.CheckInOutList(checkIns);
                return checkInsList;
            }
            else
            {
                var checkIns = _checkInOutRepository.GetCheckIns().Where(i => i.Item.Id == itemId);
                var checkInsList = checkInOutMapping.CheckInOutList(checkIns);
                return checkInsList;
            }
        }
        public List<CheckInOutForListVM> GetCheckOuts(int itemId)
        {
            if (itemId == 0)
            {
                var checkOuts = _checkInOutRepository.GetCheckOuts();
                var checkOutsList = checkInOutMapping.CheckInOutList(checkOuts);
                return checkOutsList;
            }
            else
            {
                var checkOuts = _checkInOutRepository.GetCheckOuts().Where(i => i.Item.Id == itemId);
                var checkOutsList = checkInOutMapping.CheckInOutList(checkOuts);
                return checkOutsList;
            }
        }
    }
}
