using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services;
using AutoMapper;
using Ninject;
using Domain;
using Api.ViewModels;
namespace Api.App_Start
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IArticulosService>().To<ArticulosService>();


            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            Bind<IMapper>().ToMethod(ctx =>
                    new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                //cfg.AddMaps(GetType().Assembly);
                cfg.CreateMap<PostArticuloViewModel, Articulo>();
                cfg.CreateMap<Articulo, PostArticuloViewModel>();
            });

            return config;
        }
    }

}