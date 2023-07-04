using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
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
        return Ok(user);
    }

}
