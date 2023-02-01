namespace NinthAgeCmsToArmyBook.ArmyBooks;

public class Unit
{
    public List<GlobalProfile> GlobalProfile { get; set; }
    public List<DefensiveProfile> DefensiveProfile { get; set; }
    public List<OffensiveProfile> OffensiveProfile { get; set; }
    public bool IsMount { get; set; }
    public string Name { get; set; }

    public static Unit Init()
    {
        return new Unit
        {
            Name = "New unit",
            GlobalProfile = new List<GlobalProfile>
            {
                new(4, 8, 8)
            },
            DefensiveProfile = new List<DefensiveProfile>
            {
                new(1, 3, 3, 0)
            },
            OffensiveProfile = new List<OffensiveProfile>
            {
                new(1, 3, 3, 0, 3)
            },
            IsMount = false
        };
    }
}