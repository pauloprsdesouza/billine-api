using Billine.Admin.Domain.OrderItems;
using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;
using Billine.Admin.Infrastructure.Database.DataModel.OrderItems;
using EfficientDynamoDb.Attributes;
using System;
using System.Collections.Generic;

namespace Billine.Admin.Infrastructure.Database.DataModel.Orders
{
    public class OrderModel : BaseModel
    {
        [DynamoDbProperty("Id")]
        public string Id { get; set; }

        [DynamoDbProperty("UserId")]
        public Guid UserId { get; set; }

        [DynamoDbProperty("CompanyCNPJ")]
        public string CompanyCNPJ { get; set; }

        [DynamoDbProperty("CompanyName")]
        public string CompanyName { get; set; }

        [DynamoDbProperty("Total")]
        public decimal Total { get; set; }

        [DynamoDbProperty("Items")]
        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
    }
}
