using EfficientDynamoDb.Converters;
using EfficientDynamoDb.DocumentModel;
using System;
using System.Globalization;

namespace Billine.Admin.Infrastructure.Database.Converters
{
    public class DateTimeOffsetConverter : DdbConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(in AttributeValue attributeValue)
        {
            string entryAsIso8601 = attributeValue.AsString();

            return DateTimeOffset.Parse(entryAsIso8601, null, DateTimeStyles.RoundtripKind);
        }

        public override AttributeValue Write(ref DateTimeOffset value)
        {
            return ((DateTimeOffset)value).ToString("o");
        }
    }
}
