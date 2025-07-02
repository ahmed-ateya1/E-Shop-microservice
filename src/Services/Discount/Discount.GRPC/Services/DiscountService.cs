using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services
{
    public class DiscountService(DiscountDbContext db) : DiscountProtoService.DiscountProtoServiceBase
    {

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await db.Coupons
                .FirstOrDefaultAsync(
                x => x.ProductName.ToLower() == request.ProductName.ToLower()
                );

            if (coupon == null)
            {
               return null;
            }

            return coupon.Adapt<CouponModel>();

        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Coupon.ProductName) || request.Coupon.Amount <= 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid discount request."));
            }
            var coupon = request.Coupon.Adapt<Coupon>();

            await db.Coupons.AddAsync(coupon);

            await db.SaveChangesAsync();

            return coupon.Adapt<CouponModel>();

        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = db.Coupons.FirstOrDefault(x => x.Id == request.Coupon.Id);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ID {request.Coupon.Id} not found."));
            }

            coupon.ProductName = request.Coupon.ProductName;
            coupon.Description = request.Coupon.Description;
            coupon.Amount = request.Coupon.Amount;


            await db.SaveChangesAsync();

            return coupon.Adapt<CouponModel>();
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon =await db.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName {request.ProductName} not found."));
            }

            db.Coupons.Remove(coupon);

            await db.SaveChangesAsync();
            return new DeleteDiscountResponse
            {
                Success = true,
            };

        }
    }
}
