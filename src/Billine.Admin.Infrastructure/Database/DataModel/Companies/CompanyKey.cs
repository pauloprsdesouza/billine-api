using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;

namespace Billine.Admin.Infrastructure.Database.DataModel.Companies
{
    public class CompanyKey : BaseKey<CompanyModel>
    {
        public CompanyKey(string name)
        {
            PK = $"COMPANY";
            SK = $"NAME#{name}";
        }
    }
}
