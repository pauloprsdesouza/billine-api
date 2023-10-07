using System;
using Billine.Admin.Domain.Products;

namespace Billine.Admin.Domain.OrderItems
{
    public class OrderItem
    {
        public string Description { get; set; }
        public UnityMeasure UnityMeasure { get; set; }
        public string ExternalId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnityPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
