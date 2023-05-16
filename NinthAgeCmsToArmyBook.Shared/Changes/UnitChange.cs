using System.Collections.Generic;
using System.Linq;
using NinthAgeCmsToArmyBook.Shared.ArmyBooks;

namespace NinthAgeCmsToArmyBook.Shared.Changes;

public class UnitChange : IPrintable
{
    public UnitChange()
    {
    }
    
    public UnitChange(Unit unit, BasePrizeChange basePrizeChange, AdditionalModelPrizeChange additionalModelPrizeChange, List<ProfileChange> profileChanges)
    {
        UnitName = unit.Name;
        BasePrizeChange = basePrizeChange;
        AdditionalModelPrizeChange = additionalModelPrizeChange;
        ProfileChanges = profileChanges;
    }
    
    public string UnitName { get; set; }
    public BasePrizeChange BasePrizeChange { get; set; }
    public AdditionalModelPrizeChange AdditionalModelPrizeChange { get; set; }
    public List<ProfileChange> ProfileChanges { get; set; } = new();
    
    public virtual string Print()
    {
        var printables = ProfileChanges.AsEnumerable<IPrintable>().ToList();
        if (BasePrizeChange != null) printables.Add(BasePrizeChange);
        if (AdditionalModelPrizeChange != null) printables.Add(AdditionalModelPrizeChange);
            
        return $"{UnitName}: {string.Join(", ", printables.Select(p => p.Print()))}";
    }
}