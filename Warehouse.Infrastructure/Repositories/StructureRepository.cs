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
            var structures = _context.Structures.Where(i => i.IsDeleted == false).AsQueryable();
            return structures;
        }

        public int AddStructure(Structure structure, string userId)
        {
            _context.Structures.Add(structure);
            _context.SaveChanges(userId);
            return structure.Id;
        }

        public int UpdateStructure(Structure structure, int structureId, string userId)
        {
            var s = _context.Structures.Find(structureId);
            if (s != null)
            {
                _context.Structures.Update(structure);
                _context.SaveChanges(userId);
            }
            return s.Id;
        }
    }
}
