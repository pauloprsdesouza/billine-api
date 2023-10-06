using System;

namespace Billine.Admin.Domain.Products
{
    public class Product
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string CompanyCNPJ { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public UnityMeasure UnityMeasure { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
