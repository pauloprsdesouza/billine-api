using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;
using EfficientDynamoDb.Attributes;
using System;

namespace Billine.Admin.Infrastructure.Database.DataModel.Users
{
    public class UserModel : BaseModel
    {
        [DynamoDbProperty("Id")]
        public Guid Id { get; set; }

        [DynamoDbProperty("Name")]
        public string Name { get; set; }

        [DynamoDbProperty("Email")]
        public string Email { get; set; }

        [DynamoDbProperty("Password")]
        public string Password { get; set; }
    }
}
