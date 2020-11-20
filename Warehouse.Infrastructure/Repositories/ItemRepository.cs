using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Common;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly Context _context;
        public ItemRepository(Context context)
        {
            _context = context;
        }

        private Item PutAuditableData(Item item)
        {
            if (item.Id == 0)
            {
                item.CreatedById = "new";
                item.CreatedDateTime = DateTime.Now;
                item.ModifiedById = "new";
                item.ModifiedDateTime = DateTime.Now;
            }
            else
            {
                item.ModifiedById = "mod";
                item.ModifiedDateTime = DateTime.Now;
            }
            return item;
        }

        //CRUD:
        public int AddItem(Item item)
        {
            PutAuditableData(item);
            _context.Items.Add(item);
            _context.SaveChanges();
            return item.Id;
        }

        public IQueryable<Item> GetItemsByStructure(int structureId)
        {
            var items = _context.Items
                .Where(i => i.ItemStructures.Where(s => s.StructureId == structureId).Any()).AsQueryable();
            return items;
        }
        public IQueryable<Item> GetItemsByCategory(int categoryId)
        {
            var items = _context.Items.Where(i => i.Category.Id == categoryId).AsQueryable();
            return items;
        }
        public IQueryable<Item> GetItems()
        {
            var items = _context.Items.Include(c => c.Category).AsQueryable();
            return items;
        }
        public Item GetItemById(int id)
        {
            var item = _context.Items
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .Include(st => st.ItemStructures).ThenInclude(s => s.Structure)
                .Include(c => c.CheckIns)
                .Include(c => c.CheckOuts)
                .FirstOrDefault(i => i.Id == id);
            return item;
        }
        public int UpdateItem(Item item, int itemId)
        {
            PutAuditableData(item);
            var i = _context.Items.Find(itemId);
            if (i != null)
            {
                _context.Items.Update(item);
                _context.SaveChanges();
            }
            return i.Id;
        }
        public void DeleteItem(int itemId)
        {
            //TODO: change status IsDeleted! Repository void DeteleItem for admin.
            var item = _context.Items.Find(itemId);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
        }
        public Category GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            return category;
        }
    }
}
