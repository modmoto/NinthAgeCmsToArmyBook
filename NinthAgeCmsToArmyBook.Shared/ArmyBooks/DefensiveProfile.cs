namespace NinthAgeCmsToArmyBook.ArmyBooks;

public class DefensiveProfile : BaseProfile
{
    public DefensiveProfile(int healthPoints, int defense, int resilience, int armor)
    {
        HealthPoints = healthPoints;
        Defense = defense;
        Resilience = resilience;
        Armor = armor;
    }
    
    public int HealthPoints { get; set; }
    public int Defense { get; set; }
    public int Resilience { get; set; }
    public int Armor { get; set; }
}