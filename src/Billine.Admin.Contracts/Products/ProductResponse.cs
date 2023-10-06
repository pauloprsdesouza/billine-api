using Billine.Admin.Domain.Products;
using System;

namespace Billine.Admin.Contracts.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string CompanyCNPJ { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public UnityMeasure UnityMeasure { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
