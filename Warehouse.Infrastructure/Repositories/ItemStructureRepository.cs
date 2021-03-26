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

        public IQueryable<ItemStructure> GetAllStructures()
        {
            var structures = _context.ItemStructure;
            return structures;
        }

        public IQueryable<ItemStructure> GetAllItemStructuresByItemId(int itemId)
        {
            var itemStructures = _context.ItemStructure.Where(i => i.ItemId == itemId);
            return itemStructures;
        }

        public int AddItemToStructures(List<ItemStructure> itemStructures, int itemId, string userId)
        {

            var item = _context.Items.Where(i => i.Id == itemId).Include(s => s.ItemStructures).ThenInclude(s => s.Structure).First();

            //Auditable data from repository - open tracking db.
            foreach (var its in itemStructures)
            {
                var itemStructure = item.ItemStructures.FirstOrDefault(s => s.StructureId == its.StructureId);

                if (its.ItemQuantity > 0)
                {
                    if (!item.ItemStructures.Where(s => s.StructureId == its.StructureId).Any())
                    {
                        item.ItemStructures.Add(new ItemStructure()
                        {
                            ItemId = its.ItemId,
                            StructureId = its.StructureId,
                            ItemQuantity = its.ItemQuantity,
                            CreatedById = userId,
                            ModifiedById = userId,
                            CreatedDateTime = DateTime.Now,
                            ModifiedDateTime = DateTime.Now
                        });
                    }
                    else
                    {
                        itemStructure.ItemId = its.ItemId;
                        itemStructure.StructureId = its.StructureId;
                        itemStructure.ItemQuantity = its.ItemQuantity;
                        itemStructure.CreatedById = userId;
                        itemStructure.ModifiedById = userId;
                        itemStructure.CreatedDateTime = DateTime.Now;
                        itemStructure.ModifiedDateTime = DateTime.Now;
                    }
                }
                else
                {
                    item.ItemStructures.Remove(itemStructure);
                }
            }

            _context.SaveChanges();
            return item.Id;
        }
    }
}
