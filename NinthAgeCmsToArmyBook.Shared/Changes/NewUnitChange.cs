using NinthAgeCmsToArmyBook.Shared.ArmyBooks;

namespace NinthAgeCmsToArmyBook.Shared.Changes;

public class NewUnitChange : UnitChange
{
    public NewUnitChange(Unit currentBookUnit)
    {
        UnitName = currentBookUnit.Name;
    }

    public override string Print()
    {
        return $"New unit: {UnitName}";
    }
}