using Billine.Admin.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billine.Admin.Domain.Sefaz.Models
{
    public class NFCeItem
    {
        public string ExternalId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public UnityMeasure UnityMeasure { get; set; }
        public decimal UnityPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
