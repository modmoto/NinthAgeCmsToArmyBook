namespace NinthAgeCmsToArmyBook.Model;

public class DefensiveProfile : BaseProfile
{
    public DefensiveProfile(int healthPoints, int defense, int resillience, int armor)
    {
        HealthPoints = healthPoints;
        Defense = defense;
        Resillience = resillience;
        Armor = armor;
    }
    
    public int HealthPoints { get; set; }
    public int Defense { get; set; }
    public int Resillience { get; set; }
    public int Armor { get; set; }
}