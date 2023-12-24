using System.Text.Json.Serialization;

namespace server.Core.TransactionAggregate;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionCategoryType
{
    Food,
    Transport,
    Shopping,
    Entertainment,
    Health,
    Other
}