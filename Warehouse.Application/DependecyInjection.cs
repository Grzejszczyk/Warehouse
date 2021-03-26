using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Warehouse.Application.Interfaces;
using Warehouse.Application.Services;
using Warehouse.Application.ViewModels.Item;
using Warehouse.Application.ViewModels.Structure;
using Warehouse.Application.ViewModels.Supplier;
using Warehouse.Domain.Interfaces;

namespace Warehouse.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IStructureService, StructureService>();
            services.AddTransient<ICheckInOutService, CheckInOutService>();

            services.AddTransient<IAdminService, AdminService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IValidator<EditItemVM>, EditItemValidator>();
            services.AddTransient<IValidator<SupplierDetailsVM>, SupplierDetailsValidator>();
            services.AddTransient<IValidator<StructureDetailsVM>, StructureDetailsValidator>();
            services.AddTransient<IValidator<StructureDetailsVM>, StructureDetailsValidator>();

            return services;
        }
    }
}
