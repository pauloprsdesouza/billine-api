using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;
using System;

namespace Billine.Admin.Infrastructure.Database.DataModel.Users
{
    public class UserKey : BaseKey<UserModel>
    {
        public UserKey(string email, Guid id)
        {
            PK = $"USER#{id}";
            SK = "PROFILE";
            GSIPK = $"USER#{email}";
            GSISK = "PROFILE";
        }
    }
}
