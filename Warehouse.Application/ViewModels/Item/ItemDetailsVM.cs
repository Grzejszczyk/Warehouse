using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Warehouse.Application.Mapping;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemDetailsVM
    {
        public int Id { get; set; }

        public string CreatedById { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string CatalogNumber { get; set; }
        public string DrawingNumber { get; set; }
        public int Quantity { get; set; }
        public int LowQuantityValue { get; set; }

        public byte[] Thumbnail { get; set; }
        public string ImageData { get; set; }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public List<StructuresForItemDetails> StructuresForItemDetails { get; set; }
        public List<CheckInsForItemDetails> CheckIns { get; set; }
        public List<CheckOutsForItemDetails> CheckOuts { get; set; }
    }
}
