using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billine.Admin.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
        Task<List<Order>> GetByLoggedUser(Guid userId);
        Task<Order> GetById(Guid userId, string qrCodeId);
        Task<List<Order>> GetByCompany(Guid userId, string name);
    }
}
