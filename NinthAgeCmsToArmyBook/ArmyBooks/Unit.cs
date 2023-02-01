namespace NinthAgeCmsToArmyBook.ArmyBooks;

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
    
    public List<GlobalProfile> GlobalProfile { get; set; }
    public List<DefensiveProfile> DefensiveProfile { get; set; }
    public List<OffensiveProfile> OffensiveProfile { get; set; }
    public bool IsMount { get; set; }
    public string Name { get; set; }
}