using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Services;
using Warehouse.Application.ViewModels.Supplier;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.Models.Entity;
using Xunit;

namespace Warehouse.Tests
{
    public class SupplierServiceTests
    {
        private readonly SupplierService _supplierService;
        private readonly Mock<ISupplierRepository> _supplierRepositoryMock = new Mock<ISupplierRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        //TODO: Solve Test for Mapping.

        public SupplierServiceTests()
        {
            _supplierService = new SupplierService(_supplierRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetSupplierDetails_Test()
        {
            //Arrange:
            var supplier = new Supplier()
            {
                Id = 1,
                Name = "TestSupplier1"
            };

            _supplierRepositoryMock.Setup(s => s.GetSupplierById(1))
                .Returns(supplier);

            //Act:
            var result = _supplierService.GetSupplierDetails(1);

            //Assert:
            Assert.Equal(result.Id, supplier.Id);
        }
    }
}
