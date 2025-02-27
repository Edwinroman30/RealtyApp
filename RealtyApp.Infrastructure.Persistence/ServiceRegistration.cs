﻿using RealtyApp.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealtyApp.Core.Application.Interfaces.Repositories;
using RealtyApp.Infrastructure.Persistence.Contexts;
using RealtyApp.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Infrastructure.Persistence
{

    //Extension Method - Decorator
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
                });
            }
            #endregion

            #region Repositories

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IImmovableAssetRepository, ImmovableAssetRepository>();
            services.AddTransient<IFavoriteImmovableRepository, FavoriteImmovableRepository>();
            services.AddTransient<IImprovementRepository, ImprovementRepository>();
            services.AddTransient<IImmovableAssetTypeRepository, ImmovableAssetTypeRepository>();
            services.AddTransient<ISellTypeRepository, SellTypeRepository>();
            services.AddTransient<IImprovement_ImmovableRepository, Improvement_ImmovableRepository>();
            #endregion
        }
    }
}
