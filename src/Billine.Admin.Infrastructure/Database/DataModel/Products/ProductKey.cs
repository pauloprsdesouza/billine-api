using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;
using System;

namespace Billine.Admin.Infrastructure.Database.DataModel.Products
{
    public class ProductKey : BaseKey<ProductModel>
    {
        public ProductKey(string description, DateTimeOffset createdAt)
        {
            PK = $"PRODUCT";
            SK = $"DESCRIPTION#{description}#DATE#{createdAt}";
        }
    }
}
