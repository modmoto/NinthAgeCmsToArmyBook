using System.Collections.Generic;
using System.Linq;
using NinthAgeCmsToArmyBook.ArmyBooks;

namespace NinthAgeCmsToArmyBook.Changes;

public class ChangeManager
{
    public List<ProfileChange> CreateProfileChange(Unit newBookUnit, Unit oldBookUnit)
    {
        var profileChanges = new List<ProfileChange>();
        return profileChanges;
    }

    public List<PrizeChange> CreatePriceChange(Unit newBookUnit, Unit oldBookUnit)
    {
        var prizeChanges = new List<PrizeChange>();
        if (newBookUnit.BaseCost != oldBookUnit.BaseCost)
        {
            prizeChanges.Add(new BasePrizeChange(oldBookUnit.BaseCost, newBookUnit.BaseCost));
        }

        if (newBookUnit.AdditionalCost != oldBookUnit.AdditionalCost)
        {
            prizeChanges.Add(new AdditionalModelPrizeChange(oldBookUnit.AdditionalCost, newBookUnit.AdditionalCost));
        }

        return prizeChanges;
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
                var priceChange = CreatePriceChange(currentBookUnit, oldBookUnit);
                if (profileChange.Any() | priceChange.Any())
                {
                    changesToLastVersion.Add(new UnitChange(currentBookUnit, priceChange, profileChange));
                }
            }
        }

        return changesToLastVersion;
    }
}