﻿using AutoMapper;
using Billine.Admin.Domain.Companies;
using Billine.Admin.Domain.Orders;
using Billine.Admin.Domain.Products;
using Billine.Admin.Domain.Sefaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billine.Admin.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISefazProvider _sefazProvider;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, ISefazProvider sefazProvider, ICompanyRepository companyRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _sefazProvider = sefazProvider;
            _companyRepository = companyRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Order> Create(Guid userId, string qrCodeId)
        {
            var orderRegistered = await _orderRepository.GetById(userId, qrCodeId);
            if (orderRegistered is not null) return orderRegistered;

            var order = await _sefazProvider.GetNFCEs(qrCodeId);
            if (order is null)
            {
                return null;
            }

            var company = await _companyRepository.GetByCNPJ(order.CompanyCNPJ);
            if (company is null)
            {
                company = new Company()
                {
                    CNPJ = order.CompanyCNPJ,
                    Name = order.CompanyName
                };

                await _companyRepository.Create(company);
            }

            var externalIds = order.Items.Select(x => x.ExternalId).ToList();
            var products = await _productRepository.GetByExternalIds(externalIds);

            if (!products.Any())
            {
                var productsToRegister = _mapper.Map<List<Product>>(order.Items.Where(x => !products.Select(y => y.ExternalId).Contains(x.ExternalId)).ToList().Distinct());
                await _productRepository.BatchWrite(productsToRegister);
            }

            order.QrCodeId = qrCodeId;
            order.UserId = userId;
            return await _orderRepository.Create(order);
        }

        public async Task<List<Order>> GetByCompany(Guid userId, string name)
        {
            return await _orderRepository.GetByCompany(userId, name);
        }

        public async Task<Order> GetById(Guid userId, string qrCodeId)
        {
            return await _orderRepository.GetById(userId, qrCodeId);
        }

        public async Task<List<Order>> GetByLoggedUser(Guid userId)
        {
            return await _orderRepository.GetByLoggedUser(userId);
        }
    }
}
