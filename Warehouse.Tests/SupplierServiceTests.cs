using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Mapping;
using Warehouse.Application.Services;
using Warehouse.Application.ViewModels.Supplier;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;
using Xunit;

namespace Warehouse.Tests
{
    public class SupplierServiceTests
    {
        [Fact]
        public void GetSupplierDetails_Test()
        {
            //Arrange:
            var supplier = new Supplier()
            {
                Id = 1,
                Name = "TestSupplier1"
            };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = config.CreateMapper();

            var _supplierRepositoryMock = new Mock<ISupplierRepository>();
            _supplierRepositoryMock.Setup(s => s.GetSupplierById(1))
                .Returns(supplier);

            var _supplierService = new SupplierService(_supplierRepositoryMock.Object, mapper);

            //Act:
            var result = _supplierService.GetSupplierDetails(1);

            //Assert:
            Assert.Equal(result.Id, supplier.Id);
        }
    }
}
