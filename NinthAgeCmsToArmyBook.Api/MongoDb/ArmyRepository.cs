using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using NinthAgeCmsToArmyBook.Shared.ArmyBooks;

namespace NinthAgeCmsToArmyBook.Api.MongoDb;

public class ArmyRepository : MongoDbRepositoryBase
{
    public ArmyRepository(MongoClient mongoClient) : base(mongoClient)
    {
    }

    public Task<List<ArmyBook>> LoadArmies()
    {
        return LoadAll<ArmyBook>();
    }

    public Task InitArmies()
    {
        var armyBooks = new List<ArmyBook>
        {
            ArmyBook.Init("Beast Herds"),
            ArmyBook.Init("Daemon Legions"),
            ArmyBook.Init("Dread Elves"),
            ArmyBook.Init("Dwarven Holds"),
            ArmyBook.Init("Empire of Sonnstahl"),
            ArmyBook.Init("Highborn Elves"),
            ArmyBook.Init("Infernal Dwarves"),
            ArmyBook.Init("Kingdom of Equitaine"),
            ArmyBook.Init("Ogre Khans"),
            ArmyBook.Init("Ors and Goblins"),
            ArmyBook.Init("Saurian Ancients"),
            ArmyBook.Init("Sylvan Elves"),
            ArmyBook.Init("Undying Dynasties"),
            ArmyBook.Init("Vampire Covenant"),
            ArmyBook.Init("The Vermin Swarm"),
            ArmyBook.Init("Warriors of the Dark Gods"),
            ArmyBook.Init("Åsklanders"),
            ArmyBook.Init("Makhar"),
            ArmyBook.Init("Cultists"),
            ArmyBook.Init("Legions of Sin"),
            ArmyBook.Init("Hobgoblins"),
            ArmyBook.Init("Giants of the 9th Age")
        };

        return Insert(armyBooks);
    }

    public Task<ArmyBook> LoadArmy(ObjectId objectId)
    {
        return LoadFirst<ArmyBook>(objectId);
    }

    public Task<bool> Update(ArmyBook army)
    {
        return UpdateVersionsave(army);
    }

    public Task<List<ArmyBook>> LoadArmiesByName(string nameFilter)
    {
        return LoadAll<ArmyBook>(a => a.ArmyName == nameFilter);
    }

    public Task Create(ArmyBook armyBook)
    {
        return Insert(armyBook);
    }
}