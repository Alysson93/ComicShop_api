using System.ComponentModel.DataAnnotations;

public class Comic
{

	public Guid Id { get; set; }
	public string Title { get; set; }
	public int Issue { get; set; }
	public string Author { get; set; }
	public int Year { get; set; }
	public string Description { get; set; }
	public float Price { get; set; }
	public int Quantity { get; set; }
	public bool IsRare { get; set; }

}

public record CreateComicDto (
	string Title,
	int Issue,
	string Author,
	int Year,
	string Description,
	float Price,
	int Quantity,
	bool IsRare
);