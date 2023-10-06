using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billine.Admin.Domain.Companies
{
    public interface ICompanyRepository
    {
        Task<Company> GetByCNPJ(string cnpj);
        Task<List<Company>> GetByName(string name);
        Task<List<Company>> GetAll();
        Task<Company> Create(Company company);
    }
}
