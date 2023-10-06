using System;

namespace Billine.Admin.Domain.Companies
{
    public class Company
    {
        public string CNPJ { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
