using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Infrastructure.Repositories
{
    public class ItemStructureRepository : IItemStructureRepository
    {
        private readonly Context _context;
        public ItemStructureRepository(Context context)
        {
            _context = context;
        }

        //TODO: Implement methods.
        public int AddItemToStructure(int itemId, int structureId, int itemsQty, string userId)
        {
            //not used.
            var itemStructure = new ItemStructure();
            itemStructure.ItemId = itemId;
            itemStructure.StructureId = structureId;
            _context.ItemStructure.Add(itemStructure);
            return itemStructure.ItemId;
        }
        public IQueryable<Structure> GetAllStructures()
        {
            var itemStructures = _context.ItemStructure.Where(i=>i.ItemId==1);
            var structures = _context.Structures;

            return structures;
        }

        public IQueryable<ItemStructure> GetAllItemStructuresForItem(int itemId)
        {
            var itemStructures = _context.ItemStructure.Where(i=>i.ItemId == itemId);
            return itemStructures;
        }

        public IQueryable<ItemStructure> GetStructuresForItem(int itemId)
        {
            var structures = _context.ItemStructure.Where(i => i.ItemId == itemId).Include(itS=>itS.ItemQuantity).AsQueryable();
            return structures;
        }

        public int AddItemToManyStructures(List<ItemStructure> itemStructures, string userId)
        {
            _context.ItemStructure.AddRange(itemStructures);
            //TODO: add userId.
            _context.SaveChanges();
            return itemStructures[0].ItemId;
        }


        public int AddManyItemsToStructure(int[] itemIds, int structureId, int itemsQty, string userId)
        {
            throw new NotImplementedException();
        }

        public int RemoveItemFromStructure(int itemId, int structureId, string userId)
        {
            throw new NotImplementedException();
        }

    }
}
