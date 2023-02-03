namespace NinthAgeCmsToArmyBook.ArmyBooks;

public class Unit
{
    public List<GlobalProfile> GlobalProfile { get; set; }
    public List<DefensiveProfile> DefensiveProfile { get; set; }
    public List<OffensiveProfile> OffensiveProfile { get; set; }
    public bool IsMount { get; set; }
    public int BaseCost => UnitPrize.Min;
    public int AdditionalCost => UnitPrize.Max;
    public MinMax UnitSize { get; set; }
    public MinMax UnitsPerArmy { get; set; }
    public MinMax ModelsPerArmy { get; set; }
    public MinMax UnitPrize { get; set; }
    public string Name { get; set; }

    public static Unit Init(List<Unit> oldUnits)
    {
        var newName = "new unit";
        
        if (oldUnits.Any(u => u.Name == newName))
        {
            var newUnits = oldUnits.Where(u => u.Name.StartsWith("new unit"));
            var lastNumber = newUnits.OrderBy(u => u.Name).Last().Name.Split(" ").LastOrDefault();
            if (lastNumber == "unit")
            {
                newName += " 1";
            }
            
            if (lastNumber != null && int.TryParse(lastNumber, out var realNumber))
            {
                newName += $" {realNumber + 1}";
            }

            
        }
        
        return new Unit
        {
            Name = newName,
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
            UnitPrize = new MinMax(120, 10)
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