using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;
using EfficientDynamoDb.Attributes;

namespace Billine.Admin.Infrastructure.Database.DataModel.Companies
{
    public class CompanyModel : BaseModel
    {
        [DynamoDbProperty("CNPJ")]
        public string CNPJ { get; set; }

        [DynamoDbProperty("Name")]
        public string Name { get; set; }
    }
}
