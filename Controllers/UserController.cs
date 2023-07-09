using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ComicShop_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly IUserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Get()
    {
        List<User> users = await userService.Read();
        return Ok(users);
    }


    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        User user = await userService.ReadById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Post([FromBody] CreateUserDto u)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        User userExists = await userService.ReadByEmail(u.Email);
        if (userExists != null) return BadRequest("User already exists.");
        User user = await userService.Create(u);
        return CreatedAtAction(nameof(Login), new { email = user.Email }, user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LogUserDto u)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        User user = await userService.Login(u);
        if (user == null) return BadRequest("User does not exist");
        var token = TokenService.GenerateToken(user);
        user.Password = "";
        return Ok(new {user = user, token = token});
    }

}
