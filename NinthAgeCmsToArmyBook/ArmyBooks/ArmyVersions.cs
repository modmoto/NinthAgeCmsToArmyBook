using MongoDB.Bson;
using NinthAgeCmsToArmyBook.MongoDb;

namespace NinthAgeCmsToArmyBook.ArmyBooks;

public class ArmyVersions : IIdentifiable, IVersionable
{
    public string ArmyName { get; set; }
    public List<ArmyBook> Versions { get; set; }
    public ObjectId Id { get; set; }
    public int Version { get; set; }

    public ArmyVersions(string armyName, List<ArmyBook> versions)
    {
        Versions = versions;
        ArmyName = armyName;
    }

    public static ArmyVersions Init(string name)
    {
        return new ArmyVersions(name, new List<ArmyBook>
        {
            new("0.0.1", new List<Unit>())
        });
    }
}