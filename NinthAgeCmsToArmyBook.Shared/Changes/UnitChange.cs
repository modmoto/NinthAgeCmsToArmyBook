using System.Collections.Generic;
using System.Linq;
using NinthAgeCmsToArmyBook.ArmyBooks;

namespace NinthAgeCmsToArmyBook.Changes;

public class UnitChange : IPrintable
{
    public UnitChange()
    {
    }
    
    public UnitChange(Unit unit, List<PrizeChange> prizeChanges, List<ProfileChange> profileChanges)
    {
        UnitName = unit.Name;
        PrizeChanges = prizeChanges;
        ProfileChanges = profileChanges;
    }
    
    public string UnitName { get; set; }
    public List<PrizeChange> PrizeChanges { get; set; } = new();
    public List<ProfileChange> ProfileChanges { get; set; } = new();
    
    public virtual string Print()
    {
        var prizePrints = PrizeChanges.AsEnumerable<IPrintable>();
        var profilePrints = ProfileChanges.AsEnumerable<IPrintable>();
        var printables = prizePrints.ToList();
        printables.AddRange(profilePrints);
            
        return $"{UnitName}: {string.Join(", ", printables.Select(p => p.Print()))}";
    }
}