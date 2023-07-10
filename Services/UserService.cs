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


	/* 
		Cadastro de usuários com senha encriptada no banco de dados
		retorna o usuário cadastrado.
	*/
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

	/*
		Listagem de usuários.
		(disponível apenas para administradores)
	*/
	public async Task<List<User>> Read()
	{
		List<User> users = await this.context.Users.ToListAsync();
		return users;
	}

	/* 
		Retorna um usuário a partir de seu id.
		Se o usuário não existir, retorna uma instância nula.
	*/
	public async Task<User> ReadById(Guid id)
	{
		User user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
		return user;
	}

	/* 
		Método que checa a existência do usuário no banco a partir de um email específico.
		Deve ser usado para garantir que nenhum usuário será cadastrado com um email repetido.
	*/
	public async Task<User> ReadByEmail(string email)
	{
		User user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
		return user;
	}


	/* 
		Cadastro de usuários com senha encriptada no banco de dados
		retorna o usuário cadastrado e o token.
	*/
	public async Task<User> Login(LogUserDto u)
	{
		User user = await context.Users.Where(user => user.Email == u.Email).Take(1).SingleOrDefaultAsync();
		if (user == null) return user;
		if (BCrypt.Net.BCrypt.Verify(u.Password, user.Password)) return user;
		user = null;
		return user;
	}

}