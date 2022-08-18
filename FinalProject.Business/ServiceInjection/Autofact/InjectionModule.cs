using Autofac;
using FinalProject.Base;
using FinalProject.DataAccess;

namespace FinalProject.Business
{
    public class InjectionModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IBaseService<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<IDapperContext>().As<DapperContext>().InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<DpProductRespository>().As<IProductRepository>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<DpCategoryRespository>().As<ICategoryRepository>().InstancePerLifetimeScope();

            builder.RegisterType<ColorService>().As<IColorService>().InstancePerLifetimeScope();
            builder.RegisterType<DpColorRepository>().As<IColorRepository>().InstancePerLifetimeScope();

            builder.RegisterType<BrandService>().As<IBrandService>().InstancePerLifetimeScope();
            builder.RegisterType<DpBrandRepository>().As<IBrandRepository>().InstancePerLifetimeScope();

            builder.RegisterType<OfferService>().As<IOfferService>().InstancePerLifetimeScope();
            builder.RegisterType<DpOfferRepository>().As<IOfferRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<DpAppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<JWTHelper>().As<ITokenHelper>().InstancePerLifetimeScope();
            builder.RegisterType<FireAndForgetJob>().As<IFireAndForgetJob>().InstancePerLifetimeScope();
            builder.RegisterType<FileHelperManager>().As<IFileHelper>().InstancePerLifetimeScope();

        }
    }
}
