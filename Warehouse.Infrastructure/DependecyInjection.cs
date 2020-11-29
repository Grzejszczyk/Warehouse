using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Domain.Interfaces;
using Warehouse.Infrastructure.Repositories;


namespace Warehouse.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IStructureRepository, StructureRepository>();
            services.AddTransient<IItemStructureRepository, ItemStructureRepository>();
            return services;
        }
    }
}
