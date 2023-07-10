using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ComicShop_api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{

    private readonly IUserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }


    /*
        Rota Get /user
        Deve retornar todos os usuários do sistema.
        Disponível apenas para usuários autenticados como administradores
    */
    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Get()
    {
        List<User> users = await userService.Read();
        return Ok(users);
    }


    /*
        Rota Get /user/{id}
        Deve retornar um usuário a partir do seu id
        Disponível apenas para usuários autenticados como administradores
    */
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        User user = await userService.ReadById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }


    /*
        Rota POST /user
        Deve cadastrar um novo usuário.
        Recebe no corpo da requisição, um CreateUseDto
        Por padrão, o usuário cadastrado será um cliente comum.
        A alteração para 'usuário admin' (por enquanto) deve ser feita pelo mantenedor do sistema.
        Retorna o usuário cadastrado.
    */
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserDto u)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        User userExists = await userService.ReadByEmail(u.Email);
        if (userExists != null) return BadRequest("User already exists.");
        User user = await userService.Create(u);
        return CreatedAtAction(nameof(Login), new { email = user.Email }, user);
    }


    /*
        Rota POST /user/login
        Deve gerar um token bearer para autenticar o usuário.
        Recebe no corpo da requisição, um LogUseDto
        Retorna o usuário logado e o token.
    */
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
