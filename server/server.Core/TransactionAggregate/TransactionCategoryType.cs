namespace server.Core.TransactionAggregate;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionCategoryType
{
    Food,
    Bills,
    Transport,
    Shopping,
    Entertainment,
    Health,
    Other
}