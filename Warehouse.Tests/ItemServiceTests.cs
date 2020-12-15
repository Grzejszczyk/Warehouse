using System;
using Xunit;
using Warehouse.Application.Services;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Domain.Interfaces;
using AutoMapper;
using Autofac.Extras.Moq;
using System.Linq;
using Warehouse.Domain.Models.Entity;
using System.Collections.Generic;
using Warehouse.Application.Interfaces;
using Moq;
using Warehouse.Infrastructure.Repositories;

namespace Warehouse.Tests
{
    public class ItemServiceTests
    {
        [Fact]
        public void GetItemDetails_Test1()
        {
            //Arrange:

            var mockRepo = new Mock<IItemRepository>();
            mockRepo.Setup(s => s.GetItemById(1))
                .Returns(GetTestItem());

            var item = mockRepo.Object.GetItemById(1);

            var mockMapperItemVM = new Mock<IMapper>();
            mockMapperItemVM.Setup(s => s.Map<ItemDetailsVM>(item));
            //issue: itemVM is null in ItemService.

            var itemService = new ItemService(mockRepo.Object, null, null, mockMapperItemVM.Object);

            //Act:
            var result = itemService.GetItemDetails(1);

            //Assert:
            Assert.Equal(result.Id, mockRepo.Object.GetItemById(1).Id);
        }

        private Item GetTestItem()
        {
            var item = new Item() { Id = 1, Name = "TestItem1" };
            return item;
        }
    }
}
