using AutoMapper;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Warehouse.Application.Mapping;

namespace Warehouse.Application.ViewModels.Item
{
    public class ItemForItemsListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public byte[] Thumbnail { get; set; }

        //For View img src, not mapped, assigned in service method.
        public string ThumbnailData { get; set; }
    }
}
