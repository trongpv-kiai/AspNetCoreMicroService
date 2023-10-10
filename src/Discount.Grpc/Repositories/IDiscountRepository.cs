using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon?> GetDiscount(string productName);
        Task<Coupon?> CreateDiscount(Coupon coupon);
        Task<Coupon?> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);

    }
}
