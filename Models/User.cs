public class User
{

	public Guid Id { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Name { get; set; }
	public bool IsAdmin { get; set; }

}

public record CreateUserDto(
	string Email,
	string Password,
	string Name
);

public record LogUserDto(
	string Email,
	string Password
);