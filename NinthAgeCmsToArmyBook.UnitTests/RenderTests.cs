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
        var unit = Unit.Init();
        var latexRepository = new LatexRepository();
    }

    [Test]
    [Ignore("not ready")]
    public void CreatePdfWorks()
    {
        var latexRepository = new LatexRepository();
        var directory = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\TexFiles";
        latexRepository.CreateLatex(directory, "hello-world.tex", "output_Vermin_Swarms.pdf");
    }
}