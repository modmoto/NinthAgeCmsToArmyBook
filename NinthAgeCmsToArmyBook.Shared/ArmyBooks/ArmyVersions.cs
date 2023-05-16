using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NinthAgeCmsToArmyBook.Shared.MongoDb;

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
}