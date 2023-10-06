using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billine.Admin.Domain.Companies
{
    public interface ICompanyService
    {
        Task<Company> GetByCNPJ(string cnpj);
        Task<List<Company>> GetByName(string name);
        Task<List<Company>> GetAll();
    }
}
