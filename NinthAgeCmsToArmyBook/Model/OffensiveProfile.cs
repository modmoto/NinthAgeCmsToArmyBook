namespace NinthAgeCmsToArmyBook.Model;

public class OffensiveProfile : BaseProfile
{
    public OffensiveProfile(int attacks, int offense, int strength, int armorPiercing, int agility)
    {
        Attacks = attacks;
        Offense = offense;
        Strength = strength;
        ArmorPiercing = armorPiercing;
        Agility = agility;
    }
    
    public int Attacks { get; set; }
    public int Offense { get; set; }
    public int Strength { get; set; }
    public int ArmorPiercing { get; set; }
    public int Agility { get; set; }
}