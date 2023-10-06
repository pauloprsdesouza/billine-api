using System.ComponentModel.DataAnnotations;

namespace Billine.Admin.Contracts.Orders
{
    public class PostOrderRequest
    {
        [RegularExpression(@"^(\d+)\|(\d)\|(\d)\|(\d)\|([A-Fa-f0-9]+)$")]
        public string QrCodeId { get; set; }
    }
}
