using Autofac;
using FinalProject.Base;
using FinalProject.DataAccess;

namespace FinalProject.Business
{
    public class InjectionModule:Module
    {
        //Api tarafında DataAccess katmanı direk kullanmak yerine serviceleri Business katmanında tanımlayıp Api tarafında program.cs de Busines katmanını çagırarak servicelere erişiyoruz. Bu sayede DataAccess Api Katmanında direk kulanılmayacak.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IBaseService<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<DapperContext>().As<IDapperContext>().InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<DpProductRespository>().As<IProductRepository>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<DpCategoryRespository>().As<ICategoryRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AppUserRoleService>().As<IAppUserRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<DpAppUserRoleRepository>().As<IAppUserRoleRepository>().InstancePerLifetimeScope();

            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<DpRoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();

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
