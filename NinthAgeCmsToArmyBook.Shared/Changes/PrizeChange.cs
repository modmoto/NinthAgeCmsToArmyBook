namespace NinthAgeCmsToArmyBook.Changes;

public abstract class PrizeChange : IPrintable
{
    public PrizeChange(int old, int @new)
    {
        Old = old;
        New = @new;
    }
    public int Old { get; set; }
    public int New { get; set; }
    public abstract string Print();
}

public interface IPrintable
{
    public string Print();
}