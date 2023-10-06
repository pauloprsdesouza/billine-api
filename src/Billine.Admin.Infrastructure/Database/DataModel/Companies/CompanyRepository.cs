using AutoMapper;
using Billine.Admin.Domain.Companies;
using EfficientDynamoDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billine.Admin.Infrastructure.Database.DataModel.Companies
{
    public class CompanyRepository : ICompanyRepository
    {
        public readonly IDynamoDbContext _dbContext;
        public readonly IMapper _mapper;

        public CompanyRepository(IDynamoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Company> Create(Company company)
        {
            var companyKey = new CompanyKey(company.Name);

            var companyModel = _mapper.Map<CompanyModel>(company);
            companyKey.AssignTo(companyModel);

            await _dbContext.PutItemAsync(companyModel);

            return company;
        }

        public async Task<List<Company>> GetAll()
        {
            var categoryKey = new CompanyKey(default);
            var model = await _dbContext.Query<CompanyModel>().WithKeyExpression(cond => cond.On(item => item.PK).EqualTo(categoryKey.PK)).ToListAsync();

            return _mapper.Map<List<Company>>(model);
        }

        public async Task<Company> GetByCNPJ(string cnpj)
        {
            var categoryKey = new CompanyKey(default);
            var model = await _dbContext.Query<CompanyModel>().WithKeyExpression(cond => cond.On(item => item.PK).EqualTo(categoryKey.PK))
                                                        .WithFilterExpression(cond => cond.On(item => item.CNPJ).EqualTo(cnpj))
                                                        .ToListAsync();

            return _mapper.Map<Company>(model.SingleOrDefault());
        }

        public async Task<List<Company>> GetByName(string name)
        {
            var categoryKey = new CompanyKey(name);
            var model = await _dbContext.Query<CompanyModel>().WithKeyExpression(cond => cond.On(item => item.PK).EqualTo(categoryKey.PK))
                                                              .WithFilterExpression(cond => cond.On(item => item.Name).BeginsWith(name))
                                                              .ToListAsync();


            return _mapper.Map<List<Company>>(model);
        }
    }
}
