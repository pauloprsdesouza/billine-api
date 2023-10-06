using System;
using System.Collections.Generic;

namespace Billine.Admin.Domain.Sefaz.Models
{
    public class NFCe
    {
        public string CompanyCNPJ { get; set; }
        public string CompanyName { get; set; }
        public List<NFCeItem> Items { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

    }
}
