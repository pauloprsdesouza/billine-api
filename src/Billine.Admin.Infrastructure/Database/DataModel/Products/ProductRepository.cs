using AutoMapper;
using Billine.Admin.Domain.Products;
using Billine.Admin.Infrastructure.Database.Pagination;
using EfficientDynamoDb;
using EfficientDynamoDb.Operations.BatchWriteItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billine.Admin.Infrastructure.Database.DataModel.Products
{
    public class ProductRepository : IProductRepository
    {
        public readonly IDynamoDbContext _dbContext;
        public readonly IMapper _mapper;

        public ProductRepository(IDynamoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        private ProductModel MapToSave(Product product)
        {
            product.Id = $"{product.ExternalId}-{product.CompanyCNPJ}";
            product.CreatedAt = DateTimeOffset.UtcNow;

            var model = _mapper.Map<ProductModel>(product);

            ProductKey productKey = new(product.Description, product.CreatedAt);
            productKey.AssignTo(model);

            return model;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _dbContext.PutItemAsync(MapToSave(product));

            return product;
        }

        public async Task<List<Product>> GetByExternalIds(List<string> externalIds)
        {
            var productKey = new ProductKey(default, default);
            var model = await _dbContext.Query<ProductModel>().WithKeyExpression(cond => cond.On(item => item.PK).EqualTo(productKey.PK))
                                                              .ToListAsync();

            if (model.Any())
                model = model.Where(x => externalIds.Contains(x.ExternalId)).ToList();

            return _mapper.Map<List<Product>>(model);
        }

        public async Task<List<Product>> GetByDescription(string description)
        {
            var productKey = new ProductKey(description, default);
            var model = await _dbContext.Query<ProductModel>().WithKeyExpression(cond => cond.On(item => item.PK).EqualTo(productKey.PK))
                                                              .WithFilterExpression(cond => cond.On(item => item.Description).BeginsWith(description))
                                                              .ToListAsync();

            return _mapper.Map<List<Product>>(model);
        }

        public async Task<List<Product>> BatchWrite(List<Product> products)
        {
            int pages = products.Count / 25;

            for (int i = 0; i < pages; i++)
            {
                var page = i + 1;
                var productsBatch = products.Page(page, 25);

                var batchItems = new List<IBatchWriteBuilder>();

                foreach (var product in productsBatch)
                    batchItems.Add(Batch.PutItem(MapToSave(product)));

                await _dbContext.BatchWrite().WithItems(batchItems).ExecuteAsync();
            }

            return products;
        }
    }
}
