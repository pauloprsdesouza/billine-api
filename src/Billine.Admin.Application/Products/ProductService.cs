using Billine.Admin.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billine.Admin.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _repository.CreateAsync(product);
        }

        public async Task<List<Product>> GetByDescription(string description)
        {
            return await _repository.GetByDescription(description);
        }
    }
}
