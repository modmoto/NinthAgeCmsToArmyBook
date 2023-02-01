using System.Reflection;
using NinthAgeCmsToArmyBook.ArmyBooks;
using NinthAgeCmsToArmyBook.Latex;
using NUnit.Framework;

namespace NinthAgeCmsToArmyBook.UnitTests;

public class RenderTests
{
    [Test]
    public void BasicRenderingWorks()
    {
        var globalProfiles = new List<GlobalProfile>
        {
            new(5, 10, 9)
        };
        var defensiveProfiles = new List<DefensiveProfile>
        {
            new(1, 4, 3, 3)
        };
        var offensiveProfiles = new List<OffensiveProfile>
        {
            new(1, 5, 4, 1, 6)
        };
        var unit = new Unit("OBsGuard", globalProfiles, defensiveProfiles, offensiveProfiles, false);

        var latexRepository = new LatexRepository();
    }

    [Test]
    public void CreatePdfWorks()
    {
        var latexRepository = new LatexRepository();
        var directory = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\TexFiles";
        latexRepository.CreateLatex(directory, "hello-world.tex", "output_Vermin_Swarms.pdf");
    }
}