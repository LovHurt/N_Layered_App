//using Autofac;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DataAccess.Abstract;
//using Autofac.Extras.DynamicProxy;
//using Castle.DynamicProxy;
//using Core.Utilities.Interceptors;
//using Core.Utilities.Security.JWT;
//using Microsoft.AspNetCore.Http;

//namespace Business.DependencyResolvers.Autofac
//{
//    public class AutofacBusinessModule:Module
//    {
//        protected override void Load(ContainerBuilder builder)
//        {

//            builder.RegisterType<JwtHelper>().As<ITokenHelper>();



//            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

//            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
//                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
//                {
//                    Selector = new AspectInterceptorSelector()
//                }).SingleInstance();
//        }
//    }
//}
