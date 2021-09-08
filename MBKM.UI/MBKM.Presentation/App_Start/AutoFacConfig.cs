using Autofac.Integration.Mvc;
using Autofac;
using System.Web.Mvc;
using MBKM.Presentation.Modules;

namespace MBKM.Presentation.App_Start
{
    public class AutoFacConfig
    {
        public static void ConfigureContainer()
        {
            //Autofac Configuration
            var builder = new Autofac.ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}