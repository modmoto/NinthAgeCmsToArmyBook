namespace NinthAgeCmsToArmyBook.Model;

public class Unit
{
    public Unit(string name, List<GlobalProfile> globalProfile, List<DefensiveProfile> defensiveProfile, List<OffensiveProfile> offensiveProfile, bool isMount)
    {
        Name = name;
        GlobalProfile = globalProfile;
        DefensiveProfile = defensiveProfile;
        OffensiveProfile = offensiveProfile;
        IsMount = isMount;
    }
    
    public List<GlobalProfile> GlobalProfile { get; }
    public List<DefensiveProfile> DefensiveProfile { get; }
    public List<OffensiveProfile> OffensiveProfile { get; }
    public bool IsMount { get; }
    public string Name { get; }
}