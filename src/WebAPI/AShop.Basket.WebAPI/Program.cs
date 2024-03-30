using System.Reflection;
using AShop.Basket.Application.GrpcService;
using AShop.Basket.Application.Handlers;
using AShop.Basket.Infrastructure.Repositories;
using AShop.Basket.WebAPI.Swagger;
using AShop.Common.Logging.Correlation;
using Discount.Grpc.Protos;
using HealthChecks.UI.Client;
using MassTransit;
using MassTransit.MultiBus;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(1, 0);
    option.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(option =>
{
    option.GroupNameFormat = "'v'VVV";
    option.SubstituteApiVersionInUrl = true;
});
    builder.Services.AddApiVersioning();
    builder.Services.AddCors(option =>
    {
        option.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyMethod();
        });
    });

builder.Services.AddHealthChecks();
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

builder.Services.AddMediatR(typeof(CreateShoppingCartCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(x =>
    x.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUr"]));

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  
    app.UseSwagger();
    app.UseSwaggerUI(options => {

        /*foreach (var description in .ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }*/
    });
}
        

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
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

app.Run();