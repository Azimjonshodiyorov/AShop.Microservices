using System.Reflection;
using AShop.Basket.Application.GrpcService;
using AShop.Basket.Application.Handlers;
using AShop.Basket.Infrastructure.Repositories;
using AShop.Common.Logging.Correlation;
using Discount.Grpc.Protos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    builder.Services.AddApiVersioning();
    builder.Services.AddCors(option =>
    {
        option.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyMethod();
        });
    });
});

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




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

