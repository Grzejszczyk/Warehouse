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
        public int AddStructure(Structure structure)
        {
            _context.Structures.Add(structure);
            _context.SaveChanges();
            return structure.Id;
        }

        public void DeleteStructure(int structureId)
        {
            //TODO: change status IsDeleted at Service! Repository void DeteleStructure for admin.
            var structure = _context.Structures.Find(structureId);
            if (structure != null)
            {
                _context.Structures.Remove(structure);
                _context.SaveChanges();
            }
        }

        public Structure GetStructure(int idStructure)
        {
            var structure = _context.Structures.FirstOrDefault(s => s.Id == idStructure);
            return structure;
        }

        public IQueryable<Structure> GetStructures()
        {
            var structures = _context.Structures.AsQueryable();
            return structures;
        }

        public int UpdateStructure(Structure structure, int structureId)
        {
            var s = _context.Structures.Find(structureId);
            if (s != null)
            {
                _context.Structures.Update(structure);
                _context.SaveChanges();
            }
            return s.Id;
        }
    }
}
