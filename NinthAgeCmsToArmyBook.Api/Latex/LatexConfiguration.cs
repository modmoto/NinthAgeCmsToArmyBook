namespace NinthAgeCmsToArmyBook.Api.Latex;

public class LatexConfiguration
{
    public LatexConfiguration(string latexExecutablePath)
    {
        LatexExecutablePath = latexExecutablePath;
    }
    public string LatexExecutablePath { get; }
}