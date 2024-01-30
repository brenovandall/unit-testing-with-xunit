using TestesComApi.Models;
using TestesComApi.Repositories.Interfaces;

namespace TestesComApi.Repositories.Repos
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;
        public ProductRepository()
        {
            _products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 5
                },
                new Product()
                {
                    Id = 2,
                    Name = "Milk",
                    Price = 2
                },
                new Product()
                {
                    Id = 3,
                    Name = "Chocolate",
                    Price = 2
                },
                new Product()
                {
                    Id = 4,
                    Name = "Soda",
                    Price = 4
                }
            };  
        }
        public Product Add(Product item)
        {
            if(item is not null)
            {
                _products.Add(item);
                return item;
            }

            return null;
        }

        public void Delete(int id)
        {
            var item = _products.FirstOrDefault(x => x.Id == id);
            _products.Remove(item);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }
    }
}
