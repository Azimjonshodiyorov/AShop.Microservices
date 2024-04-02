using AShop.Common.Logging;
using AShop.EventBus.Message.Common;
using AShop.EventBus.Message.Events;
using AShop.Order.Application.Extensions;
using AShop.Order.Infrastructure.Extension;
using AShop.Order.Infrastructure.OrderDbContext;
using AShop.Order.WebAPI.EventBusConsumer;
using AShop.Order.WebAPI.Extensions;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddApplicationService();
builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketOrderConsumer>();
builder.Services.AddScoped<BasketOrderConsumerV2>();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1" , new OpenApiInfo{Title = "Ordering.Api" , Version = "v1"});
});

builder.Services.AddVersionedApiExplorer(option =>
{
    option.GroupNameFormat = "'v'VVV";
    option.SubstituteApiVersionInUrl = true;
});
builder.Services.AddHealthChecks().Services.AddDbContext<OrderContext>();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketOrderConsumer>();
    config.AddConsumer<BasketOrderConsumerV2>();
    
    config.UsingRabbitMq((context, confi) =>
    {
        confi.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        confi.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue , x =>
        {
            x.ConfigureConsumer<BasketOrderConsumer>(context);
        });
        
        confi.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueueV2 , x =>
        {
            x.ConfigureConsumer<BasketOrderConsumerV2>(context);
        });
    });
});
builder.Services.AddMassTransitHostedService();
builder.Host.UseSerilog(Logging.ConfigureLogger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContexSeed>>();
    OrderContexSeed.SeedAsync(context, logger).Wait();
});

app.Run();
