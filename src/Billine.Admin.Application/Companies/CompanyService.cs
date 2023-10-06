using Billine.Admin.Domain.Companies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billine.Admin.Application.Companies
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<Company>> GetAll()
        {
            return await _companyRepository.GetAll();
        }

        public async Task<Company> GetByCNPJ(string cnpj)
        {
            return await _companyRepository.GetByCNPJ(cnpj);
        }

        public async Task<List<Company>> GetByName(string name)
        {
            return await _companyRepository.GetByName(name);
        }
    }
}
