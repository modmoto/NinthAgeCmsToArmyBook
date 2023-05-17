using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Linq;
using NinthAgeCmsToArmyBook.Shared.Contracts;

namespace NinthAgeCmsToArmyBook.Shared.ArmyBooks;

public class ArmyVersions : IIdentifiable, IVersionable
{
    public string ArmyName { get; set; }
    public List<ArmyBook> Versions { get; set; }

    private string _armyId;
    [BsonIgnore]
    public string ArmyId
    {
        get
        {
            try
            {
                return Id.ToString();
            }
            catch (Exception)
            {
                return _armyId;
            }
        }
        set => _armyId = value;
    }

    [JsonIgnore]
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

    public void ReplaceVersion(ArmyBook armyBook)
    {
        var armyVersion = Versions.SingleOrDefault(v => v.Version == armyBook.Version);
        var indexOf = Versions.IndexOf(armyVersion);
        Versions[indexOf] = armyBook;
    }
}