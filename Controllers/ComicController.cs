using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ComicShop_api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ComicController : ControllerBase
{

    private readonly IComicService comicService;

    public ComicController(ComicService comicService)
    {
        this.comicService = comicService;
    }


    /*
        Rota GET /comic
        Deve reornar a lista de todos os quadrinhos disponíveis.
    */
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Comic> comics = await comicService.Read();
        return Ok(comics);
    }

    [HttpGet("qtd")]
    public async Task<IActionResult> GetQtd([FromQuery] int skip, [FromQuery] int take)
    {
        List<Comic> comics = await comicService.ReadQtd(skip, take);
        return Ok(comics);
    }

    /*
        Rota GET /comic/{id}
        Deve retornar um quadrinho específico a partir do seu id
    */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        Comic comic = await comicService.ReadById(id);
        if (comic == null) return NotFound();
        return Ok(comic);
    }


    /*
        Rota GET /comic/author
        Deve retornar a lista de quadrinhos de um autor específico
    */
    [HttpGet("author")]
    public async Task<IActionResult> GetByAuthor([FromQuery] string author)
    {
        List<Comic> comics = await comicService.ReadByAuthor(author);
        return Ok(comics);
    }


    /*
        Rota GET /comic/year
        Deve retornar a lista de quadrinhos de um autor específico
    */
    [HttpGet("year")]
    public async Task<IActionResult> GetByYear([FromQuery] int year)
    {
        List<Comic> comics = await comicService.ReadByYear(year);
        return Ok(comics);
    }

    /*
        Rota POST /comic
        Deve cadastrar um novo quadrinho.
        Recebe um CreateComicDto no corpo de sua requisição.
        Retorna o quadrinho cadastrado.
        (apenas usuários admin)
    */
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Post([FromBody] CreateComicDto c)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        Comic comic = await comicService.Create(c);
        return CreatedAtAction(nameof(GetById), new { id = comic.Id }, comic);
    }


    /*
        Rota PUT /comic/{id}
        Deve atualizar um quadrinho a paritr do id enviado na rota.
        Recebe um CreateComicDto no corpo de sua requisição.
        (apenas usuários admin)
    */
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]

    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CreateComicDto c)
    {

        var comic = await comicService.ReadById(id);
        if (comic == null) return NotFound();
        await comicService.Update(id, c);
        return Ok();
    }


    /*
        Rota DELETE /comic/{id}
        Deve excluir um quadrinho a paritr do id enviado na rota.
        (apenas usuários admin)
    */
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var comic = await comicService.ReadById(id);
        if (comic == null) return NotFound();
        await comicService.Delete(id);
        return Ok();
    }
}
