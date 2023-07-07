public class Purchase
{

	public Guid Id { get; set; }
	public Guid ComicId { get; set; }
	public Guid UserId { get; set; }
	public string Coupon { get; set; }
	public DateTime Date { get; set; }
	public int Quantity { get; set; }
	public float TotalValue { get; set; }

}

public record PurchaseDto(
	Guid ComicId,
	Guid UserId,
	string Coupon,
	int Quantity
);