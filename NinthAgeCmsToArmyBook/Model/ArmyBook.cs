using MongoDB.Bson;

namespace NinthAgeCmsToArmyBook.Model;

public class ArmyBook
{
    public List<Unit> Units { get; }
    public string ArmyName { get; }
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    public ArmyBook(string armyName, List<Unit> units)
    {
        Units = units;
        ArmyName = armyName;
    }
}