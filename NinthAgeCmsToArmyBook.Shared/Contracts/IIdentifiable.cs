using MongoDB.Bson;

namespace NinthAgeCmsToArmyBook.Shared.Contracts;

public interface IIdentifiable
{
    public ObjectId Id { get; }
}