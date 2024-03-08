using AShop.Discount.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AShop.Discount.Infrastructure.Repositories;

public class DiscountRepasitory  :IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepasitory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async ValueTask<Coupon> GetDiscount(string productName)
    {
        await using var connection =
            new NpgsqlConnection(this._configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon = @ProductName",
            new { ProductName = productName });
        if (coupon is null)
        {
            return new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Discount Avilble" };
        }

        return coupon;
    }

    public async ValueTask<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(
            this._configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var createValue = await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName , Description , Amount) VALUES (@ProductNmae , @Description , @Amount)",
            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
        if (createValue is 0)
            return false;
        return true;
    }

    public async ValueTask<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection =
            new NpgsqlConnection(this._configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var update = await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
        new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });
        if (update is 0)
            return false;
        return true;
    }

    public async ValueTask<bool> DeleteDiscount(string productName)
    {
        await using var connection =
            new NpgsqlConnection(this._configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var delete = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
            new { ProductName = productName });
        if (delete is 0)
            return false;
        return true;
    }
}