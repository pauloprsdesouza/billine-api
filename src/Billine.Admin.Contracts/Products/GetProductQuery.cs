using System;

namespace Billine.Admin.Contracts.Products
{
    public class GetProductQuery
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public decimal GreaterThanPrice { get; set; }
        public decimal LowerThanPrice { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
