using TestesComApi.Models;

namespace TestesComApi.Repositories.Interfaces;

public interface IProductRepository
{
    List<Product> GetAll();
    Product Add(Product item);
    Product GetById(int id);
    void Delete(int id);
}
