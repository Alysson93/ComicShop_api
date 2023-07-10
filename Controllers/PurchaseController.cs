using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ComicShop_api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class PurchaseController : ControllerBase
{

    private readonly IPurchaseService purchaseService;

    public PurchaseController(PurchaseService purchaseService)
    {
        this.purchaseService = purchaseService;
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] PurchaseDto data)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try {
           Purchase purchase = await purchaseService.Create(data);
           return Ok(purchase);
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
 
    }


    [HttpGet("stock/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetStock([FromRoute] Guid id)
    {
        try {
           List<Purchase> purchases = await purchaseService.GetStock(id);
           return Ok(purchases);
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
 
    }

    [HttpGet("shop/{id}")]
    [Authorize]
    public async Task<IActionResult> GetPurchases([FromRoute] Guid id)
    {
        try {
           List<Purchase> purchases = await purchaseService.GetPurchases(id);
           return Ok(purchases);
        } catch(Exception e) {
            return BadRequest(e.Message);
        }
 
    }

}
