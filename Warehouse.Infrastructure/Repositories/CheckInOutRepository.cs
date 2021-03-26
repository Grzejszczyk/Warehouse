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

        public int CheckInItem(int itemId, int itemQty, string userName)
        {
            //TODO: Remove fake user.
            //var userId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
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
                _context.SaveChanges("Test user API"/*userId*/);
            }
            return item.Id;
        }
        public int CheckOutItem(int itemId, int itemQty, string userName)
        {
            //TODO: Remove fake user.
            //var userId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
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
                _context.SaveChanges("Fake user!"/*userId*/);
            }
            return item.Id;
        }
        public int CheckOutByStructure(int structureId, string userName)
        {
            //TODO: Remove fake user.
            //var userId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
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
            _context.SaveChanges("Fake user"/*userId*/);
            return structureId;
        }

        public IQueryable<CheckIn> GetCheckIns()
        {
            var checkIns = _context.CheckIns.AsQueryable();
            return checkIns;
        }

        public IQueryable<CheckOut> GetCheckOuts()
        {
            var checkOuts = _context.CheckOuts.AsQueryable();
            return checkOuts;
        }
    }
}
