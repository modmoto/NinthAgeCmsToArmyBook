using System.Text.Json;

namespace NinthAgeCmsToArmyBook.ArmyBooks;

public class ArmyBook
{
    public string Version { get; set; }
    public List<Unit> Units { get; set; }

    public ArmyBook(string version, List<Unit> units)
    {
        Version = version;
        Units = units;
    }

    public ArmyBook Clone()
    {
        var serialize = JsonSerializer.Serialize(this);
        var clone = JsonSerializer.Deserialize<ArmyBook>(serialize);
        var currentVersion = Version.Split(".");
        var minorVersion = int.Parse(currentVersion[2]);
        clone.Version = $"{currentVersion[0]}.{currentVersion[1]}.{minorVersion + 1}";
        return clone;
    }
}