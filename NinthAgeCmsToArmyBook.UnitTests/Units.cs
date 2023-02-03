using System.Reflection;
using NinthAgeCmsToArmyBook.ArmyBooks;
using NinthAgeCmsToArmyBook.Latex;
using NUnit.Framework;

namespace NinthAgeCmsToArmyBook.UnitTests;

public class Units
{
    [Test]
    public void UnitInitFunction()
    {
        var unit = Unit.Init(new List<Unit>());
        var init = Unit.Init(new List<Unit> { unit });
        Assert.That(init.Name, Is.EqualTo("new unit 1"));
        
        var init2 = Unit.Init(new List<Unit> { unit, init });
        Assert.That(init2.Name, Is.EqualTo("new unit 2"));

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