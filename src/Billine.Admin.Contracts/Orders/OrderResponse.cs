using Billine.Admin.Contracts.OrderItems;
using System;
using System.Collections.Generic;

namespace Billine.Admin.Contracts.Orders
{
    public class OrderResponse
    {
        public string QrCodeId { get; set; }
        public Guid UserId { get; set; }
        public string CompanyCNPJ { get; set; }
        public string CompanyName { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public List<OrderItemResponse> Items { get; set; }
    }
}
