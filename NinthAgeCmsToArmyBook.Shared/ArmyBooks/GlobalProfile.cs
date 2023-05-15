namespace NinthAgeCmsToArmyBook.ArmyBooks;

public class GlobalProfile : BaseProfile
{
    public GlobalProfile(int advance, int march, int discipline)
    {
        Advance = advance;
        March = march;
        Discipline = discipline;
    }

    public int Advance { get; set; }
    public int March { get; set; }
    public int Discipline { get; set; }
}