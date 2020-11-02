using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Services;
using Warehouse.Domain.Interfaces;

namespace Warehouse.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<ISupplierService, SupplierService>();
            return services;
        }
    }
}
