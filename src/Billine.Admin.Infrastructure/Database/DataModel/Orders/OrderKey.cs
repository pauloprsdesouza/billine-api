using Billine.Admin.Infrastructure.Database.DataModel.BaseModels;
using System;

namespace Billine.Admin.Infrastructure.Database.DataModel.Orders
{
    public class OrderKey : BaseKey<OrderModel>
    {
        public OrderKey(Guid userId, string qrCodeId)
        {
            PK = $"ORDER#USER#ID#{userId}";
            SK = $"QRCODE#ID#{qrCodeId}";
        }
    }
}
