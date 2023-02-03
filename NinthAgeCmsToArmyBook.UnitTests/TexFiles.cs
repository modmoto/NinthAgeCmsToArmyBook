using System.Reflection;
using NinthAgeCmsToArmyBook.ArmyBooks;
using NinthAgeCmsToArmyBook.Latex;
using NUnit.Framework;

namespace NinthAgeCmsToArmyBook.UnitTests;

public class TexFiles
{
    [Test]
    [Ignore("see how to install in build")]
    public void CreatePdfWorks()
    {
        var latexRepository = new LatexRepository();
        var directory = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/TexFiles";
        var outputVerminSwarmsPdf = "output_Vermin_Swarms";
        latexRepository.CreateLatex(directory, "hello-world.tex");

        var expectedPath = $"{directory}/{outputVerminSwarmsPdf}.pdf";
        Assert.IsTrue(File.Exists(expectedPath));
    }
}