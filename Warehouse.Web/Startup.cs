using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Warehouse.Infrastructure;
using Warehouse.Application;
using Warehouse.Infrastructure.Repositories;
using Warehouse.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using FluentValidation.AspNetCore;

namespace Warehouse.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>() //this must be first.
                .AddEntityFrameworkStores<Context>();

            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.ValidatorOptions.LanguageManager.Enabled = false)
                .AddFluentValidation(fv => fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false);
            services.AddRazorPages();

            services.AddApplication();
            services.AddInfrastructure();

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                {
                    //TODO: Admin managent: assigning: roles, claims to users.
                    policy.RequireRole("Admin");
                });

                options.AddPolicy("SuperUserPolicy", policy =>
                {
                    policy.RequireRole("SuperUser");
                });

                options.AddPolicy("CanManageItems", policy =>
                {
                    policy.RequireClaim("ViewItems");
                    policy.RequireClaim("EditItems");
                });
                options.AddPolicy("CanManageSuppliers", policy =>
                {
                    policy.RequireClaim("ViewSuppliers", "EditSuppliers");
                    //policy.RequireClaim("EditSuppliers");
                });
                options.AddPolicy("CanManageStructures", policy =>
                {
                    policy.RequireClaim("ViewStrucures");
                    policy.RequireClaim("EditStrucures");
                });

                options.AddPolicy("CanCheckInOut", policy =>
                {
                    policy.RequireClaim("RealizeCheckInOuts");
                });

                options.AddPolicy("CanView", policy =>
                {
                    policy.RequireClaim("ViewItems");
                    policy.RequireClaim("ViewSuppliers");
                    policy.RequireClaim("ViewStrucures");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/myLog-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
