using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Infrastructure.Repositories
{
    public class CheckInOutRepository : ICheckInOutRepository
    {
        private readonly Context _context;
        public CheckInOutRepository(Context context)
        {
            _context = context;
        }

        public int CheckInItem(int itemId, int itemQty, string userId)
        {
            Item item = _context.Items.FirstOrDefault(i => i.Id == itemId);
            CheckIn checkIn = new CheckIn()
            {
                Item = item,
                Quantity = itemQty
            };
            if (item != null)
            {
                item.Quantity = item.Quantity + itemQty;
                _context.Items.Update(item);
                _context.CheckIns.Add(checkIn);
                _context.SaveChanges(userId);
            }
            return item.Id;
        }

        public int CheckOutByStructure(int structureId, string userId)
        {
            var itemStructures = _context.ItemStructure.Where(s => s.StructureId == structureId).Include(i=>i.Item);
            List<Item> items = new List<Item>();
            List<CheckOut> checkOuts = new List<CheckOut>();

            foreach (var its in itemStructures)
            {
                if (its.Item.Quantity >= its.ItemQuantity)
                {
                    its.Item.Quantity = its.Item.Quantity - its.ItemQuantity;
                    items.Add(its.Item);
                    checkOuts.Add(new CheckOut() { Item = its.Item, Quantity = its.ItemQuantity });
                }
                else
                { return structureId; }
            }
            _context.CheckOuts.UpdateRange(checkOuts);
            _context.Items.UpdateRange(items);
            _context.SaveChanges(userId);
            return structureId;
        }

        public int CheckOutItem(int itemId, int itemQty, string userId)
        {
            Item item = _context.Items.FirstOrDefault(i => i.Id == itemId);
            CheckOut checkOut = new CheckOut()
            {
                Item = item,
                Quantity = itemQty
            };
            if (item != null)
            {
                if (item.Quantity >= itemQty)
                {
                    item.Quantity = item.Quantity - itemQty;
                }
                _context.Items.Update(item);
                _context.CheckOuts.Add(checkOut);
                _context.SaveChanges(userId);
            }
            return item.Id;
        }

        public IQueryable<Item> GetItems()
        {
            var items = _context.Items.Where(i => i.IsDeleted == false);
            return items;
        }

        public IQueryable<ItemStructure> GetItemsByStructure(int structureId)
        {
            var its = _context.ItemStructure.Where(s => s.StructureId == structureId);
            return its;
        }

        public IQueryable<Structure> GetStructures()
        {
            var structures = _context.Structures.Where(s => s.IsDeleted == false).Include(i => i.ItemStructures).ThenInclude(i => i.Item);
            return structures;
        }
    }
}
