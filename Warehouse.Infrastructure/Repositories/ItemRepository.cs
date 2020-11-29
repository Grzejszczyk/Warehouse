using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Common;
using Warehouse.Domain.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Warehouse.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly Context _context;
        public ItemRepository(Context context)
        {
            _context = context;
        }

        public int AddItem(Item item, string userId)
        {
            _context.Items.Add(item);
            _context.SaveChanges(userId);
            return item.Id;
        }

        public IQueryable<Item> GetItems()
        {
            var items = _context.Items.Where(i => i.IsDeleted == false)/*.AsQueryable()*/;
            return items;
        }
        public Item GetItemById(int id)
        {
            var item = _context.Items
                .Include(s => s.Supplier)
                .Include(st => st.ItemStructures).ThenInclude(s => s.Structure)
                .Include(c => c.CheckIns)
                .Include(c => c.CheckOuts)
                .FirstOrDefault(i => i.Id == id);
            return item;
        }
        public int UpdateItem(Item item, int itemId, string userId)
        {
            var i = _context.Items.Find(itemId);
            if (i != null)
            {
                _context.Items.Update(item);
                _context.SaveChanges(userId);
            }
            return i.Id;
        }
    }
}
