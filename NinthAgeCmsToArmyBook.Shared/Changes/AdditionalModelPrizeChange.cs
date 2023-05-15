namespace NinthAgeCmsToArmyBook.Changes;

public class AdditionalModelPrizeChange : PrizeChange
{
    public AdditionalModelPrizeChange(int old, int @new) : base(old, @new)
    {
    }

    public override string Print()
    {
        return $"additional costs {Old} -> {New}";
    }
}