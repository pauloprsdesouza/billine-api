using System;

namespace Billine.Admin.Contracts.Orders
{
    public class GetOrderQuery
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
