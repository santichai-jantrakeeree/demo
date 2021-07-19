using Core.Interfaces;
using Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Persistent;
using Persistent.Repositories.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc
{
    public partial class Startup
    {
        private void AddApplicationRepository(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDeliveryMethodRepository, DeliveryMethodRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
        }
    }
}
