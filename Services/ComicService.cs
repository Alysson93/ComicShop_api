using Microsoft.EntityFrameworkCore;

public interface IComicService {
	public Task<Comic> Create(CreateComicDto c);
	public Task<List<Comic>> Read();
	public Task<List<Comic>> ReadQtd(int skip=0, int take=25);
	public Task<Comic> ReadById(Guid id);
	public Task<List<Comic>> ReadByAuthor(string author);
	public Task<List<Comic>> ReadByYear(int year);
	public Task Update(Guid id, CreateComicDto comic);
	public Task Delete(Guid id);
}

public class ComicService : IComicService
{

	private readonly AppDbContext context;

	public ComicService(AppDbContext context)
	{
		this.context = context;
	}

	public async Task<Comic> Create(CreateComicDto c)
	{
        var comic = new Comic
        {
            Title = c.Title,
            Issue = c.Issue,
            Author = c.Author,
            Year = c.Year,
            Description = c.Description,
            Price = c.Price,
            Quantity = c.Quantity,
            IsRare = c.IsRare
        };
        await context.Comics.AddAsync(comic);
        await context.SaveChangesAsync();
        return comic;
	}

	public async Task<List<Comic>> Read()
	{
		List<Comic> comics = await this.context.Comics.ToListAsync();
		return comics;
	}

	public async Task<List<Comic>> ReadQtd(int skip=0, int take=25)
	{
		List<Comic> comics = await this.context.Comics.Skip(skip).Take(take).ToListAsync();
		return comics;
	}

	public async Task<Comic> ReadById(Guid id)
	{
		Comic comic = await context.Comics.FirstOrDefaultAsync(c => c.Id == id);
		return comic;
	}

	public async Task<List<Comic>> ReadByAuthor(string author)
	{
		List<Comic> comics = await this.context.Comics.Where(c => c.Author == author).ToListAsync();
		return comics;		
	}

	public async Task<List<Comic>> ReadByYear(int year)
	{
		List<Comic> comics = await this.context.Comics.Where(c => c.Year == year).ToListAsync();
		return comics;		
	}
	public async Task Update(Guid id, CreateComicDto c)
	{
		var comic = await this.ReadById(id);
		comic.Title = c.Title;
		comic.Issue = c.Issue;
		comic.Author = c.Author;
		comic.Year = c.Year;
		comic.Description = c.Description;
		comic.Price = c.Price;
		comic.Quantity = c.Quantity;
		comic.IsRare = c.IsRare;
		context.Comics.Update(comic);
		await context.SaveChangesAsync();
	}

	public async Task Delete(Guid id)
	{
		Comic comic = await this.ReadById(id);
		context.Comics.Remove(comic);
		await context.SaveChangesAsync();
	}

}