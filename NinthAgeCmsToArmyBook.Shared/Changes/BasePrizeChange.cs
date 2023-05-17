namespace NinthAgeCmsToArmyBook.Shared.Changes;

public class BasePrizeChange : IPrintable
{
    public BasePrizeChange(int old, int @new)
    {
        Old = old;
        New = @new;
    }
    public int Old { get; set; }
    public int New { get; set; }
    
    public string Print()
    {
        return $"base costs {Old} -> {New}";
    }
}