namespace Keysme.Web
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;

    using Data;

    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            Register(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<KeysmeDbContext>().As<IKeysmeDbContext>().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            //var servicesAssembly = Assembly.GetAssembly(typeof(IProductsService));
            //builder.RegisterAssemblyTypes(servicesAssembly).AsImplementedInterfaces();

            //builder.RegisterType<ProductsService>().As<IProductsService>().InstancePerRequest();
            //builder.RegisterType<DealsService>().As<IDealsService>().InstancePerRequest();
            //builder.RegisterType<ReportsService>().As<IReportsService>().InstancePerRequest();
            //builder.RegisterType<MissingProductsService>().As<IMissingProductsService>().InstancePerRequest();
        }
    }
}