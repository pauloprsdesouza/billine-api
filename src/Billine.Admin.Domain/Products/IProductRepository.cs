using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billine.Admin.Domain.Products
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<List<Product>> GetByDescription(string description);
        Task<List<Product>> GetByExternalIds(List<string> externalIds);
        Task<List<Product>> BatchWrite(List<Product> products);
    }
}
