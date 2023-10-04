using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Coupon?> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDb"));
            
            var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)", coupon);

            await connection.CloseAsync();
            if (affected == 0) return null;
            return coupon;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDb"));
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            await connection.CloseAsync();
            return affected > 0;
        }

        public async Task<Coupon?> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDb"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            await connection.CloseAsync();
            return coupon;
        }

        public async Task<Coupon?> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DiscountDb"));

            var affected = await connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id", coupon);

            await connection.CloseAsync();
            if (affected == 0) return null;
            return coupon;
        }
    }
}
