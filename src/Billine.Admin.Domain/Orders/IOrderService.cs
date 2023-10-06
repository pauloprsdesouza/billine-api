using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billine.Admin.Domain.Orders
{
    public interface IOrderService
    {
        Task<Order> Create(Guid userId, string qrCodeId);
        Task<List<Order>> GetByLoggedUser(Guid userId);
        Task<Order> GetById(Guid userId, string qrCodeId);
        Task<List<Order>> GetByCompany(Guid userId, string name);
    }
}
