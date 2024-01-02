using DataAccess.Abstract;
using DataAccess.Concretes;
using DataAccess.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Business.Rules;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Business.Rules.ValidationRules;
using FluentValidation;
using Business.Dtos.Requests;

namespace Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<ProductBusinessRules>();
            services.AddScoped<CategoryBusinessRules>();

            services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();



            return services;
        }

        public static IServiceCollection AddSubClassesOfType(
            this IServiceCollection services,
            Assembly assembly,
            Type type,
            Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
        )
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (var item in types)
                if (addWithLifeCycle == null)
                    services.AddScoped(item);

                else
                    addWithLifeCycle(services, type);
            return services;
        }


    }
}
