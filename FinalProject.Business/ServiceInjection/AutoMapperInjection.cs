using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Business
{
    public  static class AutoMapperInjection
    {
        public static IServiceCollection AutoMapperServiceInjection(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
            return services;
        }
    }
}
