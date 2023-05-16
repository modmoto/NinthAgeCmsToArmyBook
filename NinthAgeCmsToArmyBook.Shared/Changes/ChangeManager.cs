using System.Collections.Generic;
using System.Linq;
using NinthAgeCmsToArmyBook.Shared.ArmyBooks;

namespace NinthAgeCmsToArmyBook.Shared.Changes;

public class ChangeManager
{
    public List<ProfileChange> CreateProfileChange(Unit newBookUnit, Unit oldBookUnit)
    {
        var profileChanges = new List<ProfileChange>();
        return profileChanges;
    }

    public List<UnitChange> CreateChange(ArmyBook newBook, ArmyBook? oldBook)
    {
        var changesToLastVersion = new List<UnitChange>();
        if (oldBook == null)
        {
            return changesToLastVersion;
        }

        foreach (var currentBookUnit in newBook.Units)
        {
            var oldBookUnit = oldBook.Units.FirstOrDefault(u => u.Name == currentBookUnit.Name);
            if (oldBookUnit == null)
            {
                changesToLastVersion.Add(new NewUnitChange(currentBookUnit));
            }
            else
            {
                var profileChange = CreateProfileChange(currentBookUnit, oldBookUnit);
                var basePrizeChange = currentBookUnit.BaseCost != oldBookUnit.BaseCost ? new BasePrizeChange(oldBookUnit.BaseCost, currentBookUnit.BaseCost) : null;
                var addPrizeChange = currentBookUnit.BaseCost != oldBookUnit.BaseCost ? new AdditionalModelPrizeChange(oldBookUnit.AdditionalCost, currentBookUnit.AdditionalCost) : null;

                if (profileChange.Any() || basePrizeChange != null || addPrizeChange != null)
                {
                    changesToLastVersion.Add(new UnitChange(currentBookUnit, basePrizeChange, addPrizeChange, profileChange));
                }
            }
        }

        return changesToLastVersion;
    }
}