using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billine.Admin.Domain.Products
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product product);
        Task<List<Product>> GetByDescription(string description);
    }
}
