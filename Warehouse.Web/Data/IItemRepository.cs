using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Web.Models.Entity;

namespace Warehouse.Web.Data
{
    public interface IItemRepository
    {
        IQueryable<Item> Items { get; }
        IQueryable<Structure> Structures { get; }
    }
}
