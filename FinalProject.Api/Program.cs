using FinalProject.Api;
using FinalProject.Base;
using FinalProject.Business;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Hangfire services. PosgreSQL
HangfireInjection.HangfireServiceInjection(builder.Services);


//Default FluentValidation Filterini devre dışı bırakıp kendi yazdıgımız ValidatorFilterAttribute u ekliyoruz.
builder.Services.AddControllers(option => option.Filters.Add<ValidatorFilterAttribute>()).AddFluentValidation(x => 
x.RegisterValidatorsFromAssemblyContaining(typeof(ProductAddDtoValidator)));

//AutoMapper
AutoMapperInjection.AutoMapperServiceInjection(builder.Services);


//Swager
SwaggerShemeInjection.SwaggerShemeServiceInjection(builder.Services);


//jwt 
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Fluent Migration
FluentMigrationInjection.FluentMigrationInjectionService(builder.Services);

WebApplication app = builder.Build();

//fluent migration 
FluentMigrationInjection.FluentMigrationAplicationBuilder(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Exceptionları handler etigimiz middleware
app.UseCustomExeption();

app.UseAuthentication();

app.UseAuthorization();

//Hangfire
app.UseHangfireDashboard();

app.MapControllers();

app.Run();
