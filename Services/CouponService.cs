using Microsoft.EntityFrameworkCore;

public interface ICouponService {
	public Task<Coupon> Generate();
}

public class CouponService : ICouponService
{

	private readonly AppDbContext context;

	public CouponService(AppDbContext context)
	{
		this.context = context;
	}

	public async Task<Coupon> Generate()
	{
		Random random = new Random();
		List<bool> rareoptions = new List<bool> { false, false, false, false, false, true, false, false, false, false };
		List<int> discountoptions = new List<int> {
			10, 10, 10, 10, 10, 10, 10, 10, 10, 100,
			20, 20, 20, 20, 20, 20, 20, 20, 90, 90,
			30, 30, 30, 30, 30, 30, 30, 80, 80, 80,
			40, 40, 40, 40, 40, 40, 70, 70, 70, 70,
			50, 50, 50, 50, 50, 60, 60, 60, 60, 60
		};

		int rand = random.Next(100000, 999999);
		string code = rand.ToString("D6");
		rand = random.Next(rareoptions.Count);
		bool rare = rareoptions[rand];
		rand = random.Next(discountoptions.Count);
		int discount = discountoptions[rand];

		var coupon = new Coupon
		{
			Code = code,
			CreatedAt = DateTime.Now,
			Validate = DateTime.Now.AddDays(5),
			IsRare = rare,
			Discount = discount
		};
        await context.Coupons.AddAsync(coupon);
        await context.SaveChangesAsync();
        return coupon;
	}

}