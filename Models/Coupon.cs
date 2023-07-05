public class Coupon
{

	public int Id { get; set; }
	public string Code { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime Validate { get; set; }
	public bool IsRare { get; set; }
	public int Discount { get; set; }

}