﻿using Discount.API.Entities;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon?> GetDiscount(string productName);
        Task<Coupon?> CreateDiscount(Coupon coupon);
        Task<Coupon?> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);

    }
}
