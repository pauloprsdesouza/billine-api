﻿using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;
using EfficientDynamoDb.Attributes;
using System;

namespace Billine.Admin.Infrastructure.Database.DataModel.OrderItems
{
    public class OrderItemModel : BaseModel
    {
        [DynamoDbProperty("ProductId")]
        public Guid ProductId { get; set; }

        [DynamoDbProperty("ExternalId")]
        public string ExternalId { get; set; }

        [DynamoDbProperty("Quantity")]
        public decimal Quantity { get; set; }

        [DynamoDbProperty("UnityPrice")]
        public decimal UnityPrice { get; set; }

        [DynamoDbProperty("TotalPrice")]
        public decimal TotalPrice { get; set; }
    }
}
