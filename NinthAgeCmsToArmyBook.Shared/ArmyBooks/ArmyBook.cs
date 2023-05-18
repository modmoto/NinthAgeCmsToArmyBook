using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NinthAgeCmsToArmyBook.Shared.Contracts;

namespace NinthAgeCmsToArmyBook.Shared.ArmyBooks;

public class ArmyBook : IIdentifiable, IVersionable
{
    public string ArmyName { get; set; }
    public string BookVersion { get; set; }
    public List<Unit> Units { get; set; }

    [JsonIgnore]
    public ObjectId Id { get; set; }
    public int Version { get; set; }
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
    
    public ArmyBook Clone()
    {
        var serialize = JsonSerializer.Serialize(this);
        var clone = JsonSerializer.Deserialize<ArmyBook>(serialize);
        var currentVersion = BookVersion.Split(".");
        var minorVersion = int.Parse(currentVersion[2]);
        clone.BookVersion = $"{currentVersion[0]}.{currentVersion[1]}.{minorVersion + 1}";
        return clone;
    }
    
    public static ArmyBook Init(string name)
    {
        return new ArmyBook
        {
            ArmyName = name,
            BookVersion = "0.0.1",
            Units = new List<Unit>()
        };
    }
}