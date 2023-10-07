using AutoMapper;
using Billine.Admin.Domain.Orders;
using Billine.Admin.Infrastructure.Database.DataModel.Companies;
using EfficientDynamoDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billine.Admin.Infrastructure.Database.DataModel.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public readonly IDynamoDbContext _dbContext;
        public readonly IMapper _mapper;

        public OrderRepository(IDynamoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Order> Create(Order order)
        {
            var orderKey = new OrderKey(order.UserId, order.QrCodeId);

            var orderModel = _mapper.Map<OrderModel>(order);
            orderKey.AssignTo(orderModel);

            await _dbContext.PutItemAsync(orderModel);

            return order;
        }

        public async Task<List<Order>> GetByCompany(Guid userId, string name)
        {
            var orderKey = new OrderKey(userId, default);
            var model = await _dbContext.Query<CompanyModel>().WithKeyExpression(cond => cond.On(item => item.PK).EqualTo(orderKey.PK))
                                                              .WithFilterExpression(cond => cond.On(item => item.CNPJ).EqualTo(name))
                                                              .ToListAsync();

            return _mapper.Map<List<Order>>(model);
        }

        public async Task<Order> GetById(Guid userId, string qrCodeId)
        {
            var orderKey = new OrderKey(userId, qrCodeId);
            var model = await _dbContext.GetItemAsync<OrderModel>(orderKey.PK, orderKey.SK);

            return _mapper.Map<Order>(model);
        }

        public async Task<List<Order>> GetByLoggedUser(Guid userId)
        {
            var orderKey = new OrderKey(userId, default);
            var model = await _dbContext.Query<OrderModel>().WithKeyExpression(cond => cond.On(item => item.PK).EqualTo(orderKey.PK)).ToListAsync();

            return _mapper.Map<List<Order>>(model);
        }
    }
}
