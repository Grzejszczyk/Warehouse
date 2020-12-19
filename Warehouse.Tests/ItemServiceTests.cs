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
using Warehouse.Application.Mapping;

namespace Warehouse.Tests
{
    public class ItemServiceTests
    {
        [Fact]
        public void GetItemDetails_Test1()
        {
            //Arrange:
            var item = new Item()
            {
                Id = 1,
                Name = "TestItem1"
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = config.CreateMapper();

            var _itemRepositoryMock = new Mock<IItemRepository>();
            _itemRepositoryMock.Setup(s => s.GetItemById(1))
                .Returns(item);

            var _itemService = new ItemService(_itemRepositoryMock.Object, null, null, mapper);
            //Act:
            var result = _itemService.GetItemDetails(1);
            //Assert:
            Assert.Equal(result.Id, item.Id);
        }
    }
}
