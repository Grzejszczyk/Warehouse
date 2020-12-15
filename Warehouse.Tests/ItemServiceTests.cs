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

            //Mocking ItemRepository with methot GetItemById.
            //Returns Item.

            //Act:

            //???

            //Assert:

            //Propierties from Item equals ItemDetailsVM
        }
    }
}
