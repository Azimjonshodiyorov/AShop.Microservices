using AShop.Common.Logging;
using AShop.Discount.Infrastructure.Extensions;
using AShop.Discount.WebAPI;
using AShop.Discount.WebAPI.Service;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Host.UseSerilog(Logging.ConfigureLogger);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(
            "Communication with gRPC endpoints must be made through a gRPC client.");
    });
});
app.MigrateDatabase<Program>();
app.Run();
