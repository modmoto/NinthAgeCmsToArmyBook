namespace NinthAgeCmsToArmyBook.Shared.ArmyBooks;

public class ArmyVersions
{
    public string ArmyName { get; }
    public string BookVersion { get; }

    public ArmyVersions(string armyName, string bookVersion)
    {
        ArmyName = armyName;
        BookVersion = bookVersion;
    }
}