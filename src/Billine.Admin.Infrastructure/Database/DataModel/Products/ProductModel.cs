using Billine.Admin.Domain.Products;
using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;
using EfficientDynamoDb.Attributes;
using System;

namespace Billine.Admin.Infrastructure.Database.DataModel.Products
{
    public class ProductModel : BaseModel
    {
        [DynamoDbProperty("Id")]
        public Guid Id { get; set; }

        [DynamoDbProperty("CompanyCNPJ")]
        public string CompanyCNPJ { get; set; }

        [DynamoDbProperty("CompanyName")]
        public string CompanyName { get; set; }

        [DynamoDbProperty("Description")]
        public string Description { get; set; }

        [DynamoDbProperty("UnityMeasure")]
        public UnityMeasure UnityMeasure { get; set; }

        [DynamoDbProperty("Price")]
        public decimal Price { get; set; }
    }
}
