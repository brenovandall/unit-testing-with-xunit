using Microsoft.AspNetCore.Mvc;
using TestesComApi.Controllers;
using TestesComApi.Models;
using TestesComApi.Repositories.Interfaces;
using TestesComApi.Repositories.Repos;

namespace TestesComApi.Tests;

public class ProductsControllerTest
{
    private readonly ProductsController _controller;
    private readonly IProductRepository _respository;
    public ProductsControllerTest()
    {
        _respository = new ProductRepository();
        _controller = new ProductsController(_respository);
    }

    [Fact]
    public void GetAllTest()
    {
        var result = _controller.Get();

        Assert.IsType<OkObjectResult>(result.Result);

        var list = result.Result as OkObjectResult;

        Assert.IsType<List<Product>>(list.Value);

        var listProduts = list.Value as List<Product>;

        Assert.Equal(4, listProduts.Count);
    }

    [Fact]
    public void AddProductTest()
    {
        var product = new Product()
        {
            Id = 5,
            Name = "Test",
            Price = 100
        };

        var createdResponse = _controller.Post(product);

        Assert.IsType<CreatedAtActionResult>(createdResponse);

        var item = createdResponse as CreatedAtActionResult;
        Assert.IsType<Product>(item.Value);

        var productItem = item.Value as Product;
        Assert.Equal(product.Name, productItem.Name);
        Assert.Equal(product.Price, productItem.Price);

        var incompleteProduct = new Product()
        {
            Name = "Test"
        };

        _controller.ModelState.AddModelError("Price", "Price is a requried filed");
        var badResponse = _controller.Post(incompleteProduct);

        Assert.IsType<BadRequestObjectResult>(badResponse);

    }

    [Theory]
    [InlineData(1, 10000)]
    public void DeleteProductTest(int id1, int id2)
    {
        var validId = id1;
        var invalidId = id2;

        var notFoundResult = _controller.Remove(invalidId);

        Assert.IsType<NotFoundResult>(notFoundResult);
        Assert.Equal(4, _respository.GetAll().Count());

        var okResult = _controller.Remove(validId);

        Assert.IsType<OkResult>(okResult);
        Assert.Equal(3, _respository.GetAll().Count());
    }

    [Theory]
    [InlineData(1, 10000)]
    public void GetProductByIdTest(int id1, int id2)
    {
        var validId = id1;
        var invalidId = id2;

        var notFoundResult = _controller.Get(invalidId);
        var okResult = _controller.Get(validId);

        Assert.IsType<NotFoundResult>(notFoundResult.Result);
        Assert.IsType<OkObjectResult>(okResult.Result);

        var item = okResult.Result as OkObjectResult;

        Assert.IsType<Product>(item.Value);

        var productItem = item.Value as Product;
        Assert.Equal(validId, productItem.Id);
        Assert.Equal("Cheese", productItem.Name);
    }
}