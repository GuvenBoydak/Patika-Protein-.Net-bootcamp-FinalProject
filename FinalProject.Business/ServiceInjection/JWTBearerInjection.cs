using FinalProject.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FinalProject.Business
{
    public static class JWTBearerInjection
    {
        public static IServiceCollection JwtBearerServiceInjection(this IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            IConfiguration _configuration = provider.GetService<IConfiguration>();

            TokenOptions tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidIssuer = tokenOptions.Issuer,
                         ValidAudience = tokenOptions.Audience,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                     };
                 });
            return services;
        }
    }
}
