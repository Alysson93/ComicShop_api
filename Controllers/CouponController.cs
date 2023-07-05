using Microsoft.AspNetCore.Mvc;

namespace ComicShop_api.Controllers;

[ApiController]
[Route("[controller]")]
public class CouponController : ControllerBase
{

    private readonly ICouponService couponService;

    public CouponController(CouponService couponService)
    {
        this.couponService = couponService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        Coupon coupon = await couponService.Generate();
        return Ok(coupon);
    }

}
