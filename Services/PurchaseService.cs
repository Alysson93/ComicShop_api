using Microsoft.EntityFrameworkCore;

public interface IPurchaseService
{
	public Task<Purchase> Create(PurchaseDto data);
	public Task<List<Purchase>> GetStock(Guid id);
	public Task<List<Purchase>> GetPurchases(Guid id);
}

public class PurchaseService : IPurchaseService
{

	private readonly AppDbContext context;

	public PurchaseService(AppDbContext context)
	{
		this.context = context;
	}

	public async Task<Purchase> Create(PurchaseDto data)
	{

		Comic comic = await new ComicService(context).ReadById(data.ComicId);
		if (comic == null) throw new Exception("Comic not found");

		User user = await new UserService(context).ReadById(data.UserId);
		if (user == null) throw new Exception("User not found");

		if (data.Quantity <= 0) throw new Exception("Invalid quantity");

		if (data.Quantity > comic.Quantity) throw new Exception("Quantity exceeds the available stock.");
		comic.Quantity -= data.Quantity;
		context.Comics.Update(comic);
		await context.SaveChangesAsync();

		float discount = 1;
		Coupon coupon = await new CouponService(context).ReadByCode(data.Coupon);
		if (coupon != null) {
			if (comic.IsRare && !coupon.IsRare) discount = 1;
			else discount = coupon.Discount / 100;
		}

		Purchase purchase = new Purchase
		{
			ComicId = data.ComicId,
			UserId = data.UserId,
			Coupon = data.Coupon,
			Date = DateTime.Now,
			Quantity = data.Quantity,
			TotalValue = comic.Price * data.Quantity * discount
		};

        await context.Purchases.AddAsync(purchase);
        await context.SaveChangesAsync();
		return purchase;

	}


	public async Task<List<Purchase>> GetStock(Guid id)
	{
		List<Purchase> purchases = await this.context.Purchases.Where(p => p.ComicId == id).ToListAsync();
		return purchases;
	}

	public async Task<List<Purchase>> GetPurchases(Guid id)
	{
		List<Purchase> purchases = await this.context.Purchases.Where(p => p.UserId == id).ToListAsync();
		return purchases;
	}


}