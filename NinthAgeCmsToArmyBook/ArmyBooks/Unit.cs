namespace NinthAgeCmsToArmyBook.ArmyBooks;

public class Unit
{
    public List<GlobalProfile> GlobalProfile { get; set; }
    public List<DefensiveProfile> DefensiveProfile { get; set; }
    public List<OffensiveProfile> OffensiveProfile { get; set; }
    public bool IsMount { get; set; }
    public int BaseCost { get; set; }
    public int AdditionalCost { get; set; }
    public MinMax UnitSize { get; set; }
    public MinMax UnitsPerArmy { get; set; }
    public MinMax ModelsPerArmy { get; set; }
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
            IsMount = false,
            UnitsPerArmy = new MinMax(0, 2),
            ModelsPerArmy = new MinMax(0, 10),
            UnitSize = new MinMax(10, 20),
        };
    }
}

public class MinMax
{
    public MinMax(int min, int max)
    {
        Min = min;
        Max = max;
    }
    
    public int Min { get; set; }
    public int Max { get; set; }
}