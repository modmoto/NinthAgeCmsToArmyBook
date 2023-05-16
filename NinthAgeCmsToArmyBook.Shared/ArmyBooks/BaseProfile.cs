using System.Collections.Generic;

namespace NinthAgeCmsToArmyBook.Shared.ArmyBooks;

public class BaseProfile
{
    public List<ModelRule> ModelRules { get; set; }
    public string ProfileName { get; set; }
}