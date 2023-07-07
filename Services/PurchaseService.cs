public interface IPurchaseService
{
	public Task<Purchase> Buy(PurchaseDto data);
}

public class PurchaseService : IPurchaseService
{

	private readonly AppDbContext context;

	public PurchaseService(AppDbContext context)
	{
		this.context = context;
	}

	public async Task<Purchase> Buy(PurchaseDto data)
	{
		Purchase purchase = new Purchase();
		return purchase;
	}

}