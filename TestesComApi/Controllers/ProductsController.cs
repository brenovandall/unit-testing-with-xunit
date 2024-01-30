using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestesComApi.Models;
using TestesComApi.Repositories.Interfaces;

namespace TestesComApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repos;
    public ProductsController(IProductRepository repos)
    {
        _repos = repos;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var items = _repos.GetAll();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var item = _repos.GetById(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Product item)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var itemToAdd = _repos.Add(item);
        return CreatedAtAction("Get", new { id = item.Id }, item);
    }

    [HttpDelete("{id}")]
    public ActionResult Remove(int id)
    {
        var existingItem = _repos.GetById(id);

        if (existingItem == null)
        {
            return NotFound();
        }

        _repos.Delete(id);
        return Ok();
    }
}
