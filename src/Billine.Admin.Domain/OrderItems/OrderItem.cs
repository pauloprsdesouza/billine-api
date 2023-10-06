using System;

namespace Billine.Admin.Domain.OrderItems
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public string ExternalId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnityPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
