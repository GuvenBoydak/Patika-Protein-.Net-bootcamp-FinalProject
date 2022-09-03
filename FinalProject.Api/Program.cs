using Autofac;
using Autofac.Extensions.DependencyInjection;
using FinalProject.Api;
using FinalProject.Business;
using FluentValidation.AspNetCore;
using Hangfire;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Hangfire services. PostgreSQL
//HangfireInjection.HangfireServiceInjection(builder.Services);


builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:7137", "https://localhost:7137").AllowAnyMethod().
AllowAnyHeader()
));

//Default FluentValidation Filterini devre dışı bırakıp kendi yazdıgımız ValidatorFilterAttribute u ekliyoruz.
builder.Services.AddControllers(option => option.Filters.Add<ValidatorFilterAttribute>()).AddFluentValidation(x => 
x.RegisterValidatorsFromAssemblyContaining(typeof(ProductAddDtoValidator)));

//AutoMapper
AutoMapperInjection.AutoMapperServiceInjection(builder.Services);

//Autofact
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new InjectionModule()));

//Swagger
SwaggerShemeInjection.SwaggerShemeServiceInjection(builder.Services);


//jwt 
JWTBearerInjection.JwtBearerServiceInjection(builder.Services);

//Serilog
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
Log.Information("Application is starting.");


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

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

//Hangfire
//app.UseHangfireDashboard();

app.MapControllers();

app.Run();
