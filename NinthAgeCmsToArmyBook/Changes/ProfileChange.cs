namespace NinthAgeCmsToArmyBook.Changes;

public class ProfileChange : IPrintable
{
    public ProfileChange(string profileType, int old, int @new)
    {
        ProfileType = profileType;
        Old = old;
        New = @new;
    }
    
    public string ProfileType { get; set; }
    public int Old { get; set; }
    public int New { get; set; }
    public string Print()
    {
        return $"{ProfileType} {Old} -> {New}";
    }
}