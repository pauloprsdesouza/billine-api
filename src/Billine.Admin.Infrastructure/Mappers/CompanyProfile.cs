using AutoMapper;
using Billine.Admin.Contracts.Companies;
using Billine.Admin.Domain.Companies;
using Billine.Admin.Infrastructure.Database.DataModel.Companies;

namespace Billine.Admin.Infrastructure.Mappers
{
    public class CompanyProfile : Profile
    {

        public CompanyProfile()
        {
            CreateMap<CompanyModel, Company>().ReverseMap();
            CreateMap<Company, CompanyResponse>();
        }
    }
}
