using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TestTask.Config;
using TestTask.Context;
using TestTask.Converters;
using TestTask.Repositories.Implementation;
using TestTask.Repositories.Interfaces;
using TestTask.Services.Implementation;
using TestTask.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<OrderDbContext>(
    opt => opt
        .UseNpgsql(builder.Configuration
            .GetConnectionString("PostgresConnectionString"))
        .EnableSensitiveDataLogging()

    );
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new ClientAutofacModule
{
    Configuration = builder.Configuration,

}));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new OrderAutofacModule
{
    Configuration = builder.Configuration,

}));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new StatisticsAutofacModule   
{
    Configuration = builder.Configuration,

}));

builder.Services.AddEndpointsApiExplorer();
object value = builder.Services.AddSwaggerGen();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
