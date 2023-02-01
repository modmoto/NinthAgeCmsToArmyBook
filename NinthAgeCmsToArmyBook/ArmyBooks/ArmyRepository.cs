using MongoDB.Bson;
using MongoDB.Driver;
using NinthAgeCmsToArmyBook.MongoDb;

namespace NinthAgeCmsToArmyBook.ArmyBooks;

public class ArmyRepository : MongoDbRepositoryBase
{
    public ArmyRepository(MongoClient mongoClient) : base(mongoClient)
    {
    }

    public Task<List<ArmyVersions>> LoadArmies()
    {
        return LoadAll<ArmyVersions>();
    }

    public Task InitArmies()
    {
        var armyBooks = new List<ArmyVersions>
        {
            ArmyVersions.Init("Beast Herds"),
            ArmyVersions.Init("Daemon Legions"),
            ArmyVersions.Init("Dread Elves"),
            ArmyVersions.Init("Dwarven Holds"),
            ArmyVersions.Init("Empire of Sonnstahl"),
            ArmyVersions.Init("Highborn Elves"),
            ArmyVersions.Init("Infernal Dwarves"),
            ArmyVersions.Init("Kingdom of Equitaine"),
            ArmyVersions.Init("Ogre Khans"),
            ArmyVersions.Init("Ors and Goblins"),
            ArmyVersions.Init("Saurian Ancients"),
            ArmyVersions.Init("Sylvan Elves"),
            ArmyVersions.Init("Undying Dynasties"),
            ArmyVersions.Init("Vampire Covenant"),
            ArmyVersions.Init("The Vermin Swarm"),
            ArmyVersions.Init("Warriors of the Dark Gods"),
            ArmyVersions.Init("Åsklanders"),
            ArmyVersions.Init("Makhar"),
            ArmyVersions.Init("Cultists"),
            ArmyVersions.Init("Legions of Sin"),
            ArmyVersions.Init("Hobgoblins"),
            ArmyVersions.Init("Giants of the 9th Age")
        };

        return Insert(armyBooks);
    }

    public Task<ArmyVersions> LoadArmy(ObjectId objectId)
    {
        return LoadFirst<ArmyVersions>(objectId);
    }
}