﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataAccess.Concretes;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<NorthwindContext>(options => options.UseInMemoryDatabase("nArchitecture"));

            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(configuration.GetConnectionString("ETrade")));

            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IUserDal, EfUserDal>();

            //services.AddScoped<IOperationClaimDal, EfOperationClaimDal>();
            //services.AddScoped<IUserOperationClaimDal, EfUserOperationClaimDal>();


            return services;
        }

    }
}
