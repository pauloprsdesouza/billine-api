using Billine.Admin.Domain.Products;
using System;

namespace Billine.Admin.Contracts.OrderItems
{
    public class OrderItemResponse
    {
        public string Description { get; set; }
        public string ExternalId { get; set; }
        public decimal Quantity { get; set; }
        public UnityMeasure UnityMeasure { get; set; }
        public decimal UnityPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
