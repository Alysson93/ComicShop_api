using Microsoft.EntityFrameworkCore;

public interface ICouponService {
	public Task<Coupon> Generate();
	public Task<Coupon> ReadByCode(string code);
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
			10, 10, 10, 10, 10,
			20, 20, 20, 20, 30,
			30, 30, 40, 40, 50
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


	public async Task<Coupon> ReadByCode(string code)
	{
		Coupon coupon = await context.Coupons.FirstOrDefaultAsync(c => c.Code == code);
		return coupon;
	}

}