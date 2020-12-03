using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Application.Interfaces;
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
        private readonly IItemRepository _itemRepository;
        private readonly IStructureRepository _structureRepository;
        private readonly ICheckInOutRepository _checkInOutRepository;
        private readonly IMapper _mapper;
        public CheckInOutService(IItemRepository itemRepo, IStructureRepository structureRepo, ICheckInOutRepository checkInOutRepo, IMapper mapper)
        {
            _itemRepository = itemRepo;
            _structureRepository = structureRepo;
            _checkInOutRepository = checkInOutRepo;
            _mapper = mapper;
        }
        public int CheckIn(int itemId, int itemQty, string userId)
        {
            _checkInOutRepository.CheckInItem(itemId, itemQty, userId);
            return itemId;
        }

        public int CheckOut(int itemId, int itemQty, string userId)
        {
            _checkInOutRepository.CheckOutItem(itemId, itemQty, userId);
            return itemId;
        }

        public int CheckOutByStructure(int structureId, string userId)
        {
            _checkInOutRepository.CheckOutByStructure(structureId, userId);
            return structureId;
        }

        public ItemsListForCheckInOutListVM GetAllItemsForCheckInOutList(int pageSize, int pageNo, string searchString)
        {
            var items = _checkInOutRepository.GetItems().Where(s => s.Name.Contains(searchString)).ProjectTo<ItemForCheckInOutListVM>(_mapper.ConfigurationProvider).ToList();
            var itemsToShow = items.Skip(pageSize * (pageNo - 1)).Take(pageSize);
            var itemsList = new ItemsListForCheckInOutListVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = items.Count() },
                Items = itemsToShow.ToList()
            };

            return itemsList;
        }

        public StrusturesListForCheckOutVM GetStructures(int pageSize, int pageNo, string searchString)
        {
            var structuresVM = _checkInOutRepository.GetStructures()
                .Where(s => s.Name.Contains(searchString))
                .ProjectTo<StructureForCheckOutVM>(_mapper.ConfigurationProvider).ToList();

            foreach (var s in structuresVM)
            {
                var its = _checkInOutRepository.GetItemsByStructure(s.StructureId);
                s.Items = its.Where(s=>s.StructureId==s.StructureId).Select(i=>i.Item).ProjectTo<ItemForCheckInOutListVM>(_mapper.ConfigurationProvider).ToList();

                foreach (var i in s.Items)
                {
                    i.CheckInOutQty = its.FirstOrDefault(it => it.ItemId == i.Id).ItemQuantity;
                }
            }
            var structuresToShow = structuresVM.Skip(pageSize * (pageNo - 1)).Take(pageSize);
            var structuresList = new StrusturesListForCheckOutVM()
            {
                PaggingInfo = new PagingInfo() { CurrentPage = pageNo, ItemsPerPage = pageSize, TotalItems = structuresVM.Count() },
                Structures = structuresToShow.ToList()
            };
            return structuresList;
        }

        public ItemsListForCheckInOutListVM GetItemsFromStructureForCheckInOutList(int structureId)
        {
            var items = _checkInOutRepository.GetItemsByStructure(structureId).ProjectTo<ItemForCheckInOutListVM>(_mapper.ConfigurationProvider).ToList();

            var itemsForStructure = new ItemsListForCheckInOutListVM() { Items = items };

            return itemsForStructure;
        }
    }
}
