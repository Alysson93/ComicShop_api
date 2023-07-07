using Microsoft.AspNetCore.Mvc;

namespace ComicShop_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ComicController : ControllerBase
{

    private readonly IComicService comicService;

    public ComicController(ComicService comicService)
    {
        this.comicService = comicService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<Comic> comics = await comicService.Read();
        return Ok(comics);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        Comic comic = await comicService.ReadById(id);
        if (comic == null) return NotFound();
        return Ok(comic);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateComicDto c)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        Comic comic = await comicService.Create(c);
        return CreatedAtAction(nameof(GetById), new { id = comic.Id }, comic);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] CreateComicDto c)
    {

        var comic = await comicService.ReadById(id);
        if (comic == null) return NotFound();
        await comicService.Update(id, c);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var comic = await comicService.ReadById(id);
        if (comic == null) return NotFound();
        await comicService.Delete(id);
        return Ok();
    }
}
