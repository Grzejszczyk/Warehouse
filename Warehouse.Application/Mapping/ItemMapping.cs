using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;

namespace Warehouse.Application.Mapping
{
    internal class ItemMapping
    {
        internal List<ItemForItemsListVM> MapItems(IQueryable<Item> items)
        {
            return items.Select(item => new ItemForItemsListVM
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                Thumbnail = item.Thumbnail
            }).ToList();
        }
        internal List<ItemStructureForListVM> MapItemStructure(IQueryable<ItemStructure> itemStructures)
        {
            return itemStructures.Select(itemStruct => new ItemStructureForListVM
            {
                StructureId = itemStruct.Id,
                StructureName = itemStruct.Structure.Name,
                ProductName = itemStruct.Structure.ProductName,
                ProjectName = itemStruct.Structure.Project,
                ItemId = itemStruct.Item.Id,
                ItemName = itemStruct.Item.Name,
                ItemQtyForStructure = itemStruct.ItemQuantity,
                ItemTotalQty = itemStruct.Item.Quantity
            }).ToList();
        }
        internal ItemDetailsVM MapItemDetails(Item item)
        {
            var itemVM = new ItemDetailsVM()
            {
                Id = item.Id,
                CreatedById = item.CreatedById,
                CreatedDateTime = item.CreatedDateTime,
                ModifiedById = item.ModifiedById,
                ModifiedDateTime = item.ModifiedDateTime,
                CatalogNumber = item.CatalogNumber,
                Description = item.Description,
                DrawingNumber = item.DrawingNumber,
                Thumbnail = item.Thumbnail,
                ImageData = "",
                LowQuantityValue = item.LowQuantityValue,
                Name = item.Name,
                Quantity = item.Quantity,
                SupplierName = item.Supplier.Name,
                SupplierId = item.Supplier.Id,
                StructuresForItemDetails = new List<StructuresForItemDetails>(),
                CheckIns = new List<CheckInsForItemDetails>(),
                CheckOuts = new List<CheckOutsForItemDetails>()
            };
            if (item.Thumbnail != null)
            {
                itemVM.ImageData = CreateImgData(item.Thumbnail);
            }

            foreach (var s in item.ItemStructures)
            {
                itemVM.StructuresForItemDetails.Add(new StructuresForItemDetails()
                {
                    StructureId = s.StructureId,
                    QuantityForStructure = s.ItemQuantity,
                    StructureName = s.Structure.Name
                });
            }

            foreach (var c in item.CheckIns)
            {
                itemVM.CheckIns.Add(new CheckInsForItemDetails()
                {
                    ActionDateTime = c.ModifiedDateTime,
                    CheckInId = c.Id,
                    Quantity = c.Quantity,
                    UserName = c.ModifiedById
                });
            }

            foreach (var c in item.CheckOuts)
            {
                itemVM.CheckOuts.Add(new CheckOutsForItemDetails()
                {
                    ActionDateTime = c.ModifiedDateTime,
                    CheckOutId = c.Id,
                    Quantity = c.Quantity,
                    UserName = c.ModifiedById
                });
            }
            return itemVM;
        }
        internal void MapItemEntityFromVM(EditItemVM itemVM, Item itemEntity)
        {
            itemEntity.Name = itemVM.Name;
            itemEntity.Description = itemVM.Description;
            itemEntity.CatalogNumber = itemVM.CatalogNumber;
            itemEntity.DrawingNumber = itemVM.DrawingNumber;
            itemEntity.Quantity = itemVM.Quantity;
            itemEntity.LowQuantityValue = itemVM.LowQuantityValue;
            itemEntity.Thumbnail = itemVM.Thumbnail;
            itemEntity.SupplierId = itemVM.SupplierId;
        }

        internal EditItemVM MapItemForEditVM(Item item)
        {
            var itemVM = new EditItemVM()
            {
                Description = item.Description,
                Id = item.Id,
                Thumbnail = item.Thumbnail,
                LowQuantityValue = item.LowQuantityValue,
                Name = item.Name,
                Quantity = item.Quantity,
                CatalogNumber = item.CatalogNumber,
                DrawingNumber = item.DrawingNumber,
                SupplierName = item.Supplier.Name,
                SupplierId = item.SupplierId
            };
            return itemVM;
        }
        internal string CreateImgData(byte[] img)
        {
            string imreBase64Data = Convert.ToBase64String(img);
            string imgDataURL = string.Format("data:image/jpg;base64,{0}", imreBase64Data);
            string ThumbnailData = imgDataURL;
            return ThumbnailData;
        }

        internal bool SaveImageAsThumbnail(EditItemVM itemVM)
        {
            var imageContent = itemVM.Thumbnail;
            using (Image itemImage = Image.Load(imageContent))
            {
                //Save Thumbnail:
                using (var thumbnailMemoryStream = new MemoryStream())
                {
                    if (thumbnailMemoryStream.Length < 5242880)
                    {
                        //memoryStream.CopyTo(thumbnailMemoryStream);
                        itemImage.Mutate(x => x.Resize(new Size(100, 100)));
                        itemImage.SaveAsJpeg(thumbnailMemoryStream);
                        var thumbnailcontent = thumbnailMemoryStream.ToArray();
                        itemVM.Thumbnail = thumbnailcontent;
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
