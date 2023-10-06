using Billine.Admin.Infrastructure.Database.Converters;
using EfficientDynamoDb.Attributes;
using System;

namespace Billine.Admin.Infrastructure.Database.DataModel.BaseModels
{
    [DynamoDbTable(DynamoDbTableSchema.Schema)]
    public class BaseModel
    {
        [DynamoDbProperty("PK", DynamoDbAttributeType.PartitionKey)]
        public string PK { get; set; }

        [DynamoDbProperty("SK", DynamoDbAttributeType.SortKey)]
        public string SK { get; set; }

        [DynamoDbProperty("GSIPK")]
        public string GSIPK { get; set; }

        [DynamoDbProperty("GSISK")]
        public string GSISK { get; set; }

        [DynamoDbProperty("CreatedAt", typeof(DateTimeOffsetConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        [DynamoDbProperty("UpdatedAt", typeof(DateTimeOffsetConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
