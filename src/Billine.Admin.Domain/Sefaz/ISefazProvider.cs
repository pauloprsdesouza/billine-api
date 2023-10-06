using Billine.Admin.Domain.Orders;
using System.Threading.Tasks;

namespace Billine.Admin.Domain.Sefaz
{
    public interface ISefazProvider
    {
        Task<Order> GetNFCEs(string key);
    }
}