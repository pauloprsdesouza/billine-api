using Billine.Admin.Domain.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billine.Admin.Domain.Orders
{
    public class Order
    {
        public string QrCodeId { get; set; }
        public Guid UserId { get; set; }
        public string CompanyCNPJ { get; set; }
        public string CompanyName { get; set; }
        public decimal Total => Items.Select(x => x.TotalPrice).Sum();
        public DateTimeOffset CreatedAt { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
