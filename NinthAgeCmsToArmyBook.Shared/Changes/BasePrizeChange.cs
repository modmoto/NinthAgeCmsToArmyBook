namespace NinthAgeCmsToArmyBook.Changes;

public class BasePrizeChange : PrizeChange
{
    public BasePrizeChange(int old, int @new) : base(old, @new)
    {
    }
    
    public override string Print()
    {
        return $"base costs {Old} -> {New}";
    }
}