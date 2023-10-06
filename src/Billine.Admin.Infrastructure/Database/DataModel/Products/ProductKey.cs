using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;
using System;

namespace Billine.Admin.Infrastructure.Database.DataModel.Products
{
    public class ProductKey : BaseKey<ProductModel>
    {
        public ProductKey(string name, DateTimeOffset createdAt)
        {
            PK = $"PRODUCT";
            SK = $"NAME${name}#DATE#{createdAt}";
        }
    }
}
