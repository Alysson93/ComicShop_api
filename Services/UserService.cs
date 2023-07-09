using Microsoft.EntityFrameworkCore;

public interface IUserService {
	public Task<User> Create(CreateUserDto u);
	public Task<List<User>> Read();
	public Task<User> ReadById(Guid id);
	public Task<User> ReadByEmail(string email);
	public Task<User> Login(LogUserDto u);
}

public class UserService : IUserService
{

	private readonly AppDbContext context;

	public UserService(AppDbContext context)
	{
		this.context = context;
	}

	public async Task<User> Create(CreateUserDto u)
	{
		string password = BCrypt.Net.BCrypt.HashPassword(u.Password);
        var user = new User
        {
            Email = u.Email,
            Password = password,
            Name = u.Name,
			Role = "client"
        };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
	}

	public async Task<List<User>> Read()
	{
		List<User> users = await this.context.Users.ToListAsync();
		return users;
	}

	public async Task<User> ReadById(Guid id)
	{
		User user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
		return user;
	}

	public async Task<User> ReadByEmail(string email)
	{
		User user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
		return user;
	}

	public async Task<User> Login(LogUserDto u)
	{
		User user = await context.Users.Where(user => user.Email == u.Email).Take(1).SingleOrDefaultAsync();
		if (user == null) return user;
		if (BCrypt.Net.BCrypt.Verify(u.Password, user.Password)) return user;
		user = null;
		return user;
	}

}