using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Infrastructure.Repositories
{
    public class StructureRepository : IStructureRepository
    {
        private readonly Context _context;
        public StructureRepository(Context context)
        {
            _context = context;
        }
        public Structure GetStructure(int idStructure)
        {
            var structure = _context.Structures.FirstOrDefault(s => s.Id == idStructure);
            return structure;
        }

        public IQueryable<Structure> GetStructures()
        {
            var structures = _context.Structures.Where(i => i.IsDeleted == false);
            return structures;
        }

        public IQueryable<Structure> GetStructuresByItemId(int itemId)
        {
            var structures = _context.ItemStructure
                .Where(s => s.ItemId == itemId)
                .Select(s=>s.Structure);

            return structures;
        }

        public int AddStructure(Structure structure, string userName)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
            _context.Structures.Add(structure);
            _context.SaveChanges(userId);
            return structure.Id;
        }

        public int EditStructure(Structure structure, int structureId, string userName)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
            var s = _context.Structures.Find(structureId);
            if (s != null)
            {
                _context.Structures.Update(structure);
                _context.SaveChanges(userId);
            }
            return s.Id;
        }

        public int EditItemsStructure(int structureId, List<ItemStructure> itemStructureList, string userName)
        {
            var userId = _context.Users.FirstOrDefault(u => u.UserName == userName).Id;
            var structure = _context.Structures.Find(structureId);
            
            if (structure != null)
            {
                if (structure.ItemStructures.Any())
                {
                    structure.ItemStructures.Clear();
                }

                structure.ItemStructures = itemStructureList;

                _context.Structures.Update(structure);
                _context.SaveChanges(userId);
            }

            return structureId;
        }
    }
}
