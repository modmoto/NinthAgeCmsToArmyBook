namespace NinthAgeCmsToArmyBook.Shared.Changes;

public class AdditionalModelPrizeChange : IPrintable
{
    public AdditionalModelPrizeChange(int old, int @new)
    {
        Old = old;
        New = @new;
    }
    public int Old { get; set; }
    public int New { get; set; }

    public string Print()
    {
        return $"additional costs {Old} -> {New}";
    }
}