using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Web.Models.Entity;

namespace Warehouse.Web.Data
{
    public class Repository : IItemRepository
    {
        IQueryable<Item> IItemRepository.Items => new List<Item> {
            new Item() {
                Id = 1,
                CreatedById = 1,
                CreatedDateTime = DateTime.Now,
                Name = "Zderzak przedni",
                Description = "Zderzak przedni stalowy",
                LowQuantityValue = 2,
                WhereUsed = 1 },
            new Item() {
                Id = 2,
                CreatedById = 1,
                CreatedDateTime = DateTime.Now,
                Name = "Zderzak tylny",
                Description = "Zderzak tylny aluminiowy",
                LowQuantityValue = 2,
                WhereUsed = 1 },
            new Item() {
                Id = 3,
                CreatedById = 1,
                CreatedDateTime = DateTime.Now,
                Name = "Wahacz",
                Description = "Wahacz poprzeczny dolny",
                LowQuantityValue = 2,
                WhereUsed = 2 }
        }.AsQueryable();

        IQueryable<Structure> IItemRepository.Structures => new List<Structure> {
            new Structure()
            {
                Id = 1,
                CreatedById = 1,
                CreatedDateTime = DateTime.Now,
                ProductName = "Pojazd specjalnego przeznaczenia",
                Project = "Projekt 1",
                Subassembly = "Nadwozie"
            },
            new Structure()
            {
                Id = 2,
                CreatedById = 1,
                CreatedDateTime = DateTime.Now,
                ProductName = "Pojazd turystyczny",
                Project = "Projekt 2",
                Subassembly = "Podwozie"
            }}.AsQueryable();
    }
}
