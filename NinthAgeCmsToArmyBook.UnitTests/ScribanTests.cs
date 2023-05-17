using System.Reflection;
using NinthAgeCmsToArmyBook.Shared.ArmyBooks;
using NUnit.Framework;
using Scriban;

namespace NinthAgeCmsToArmyBook.UnitTests;

public class ScribanTests
{
    [Test]
    public async Task UnitInitFunction()
    {
        var template = Template.Parse("test {{ Name }}");
        var unit = new Unit
        {
            Name = "Das ist cool",
            UnitPrize = new MinMax(1, 2)
        };
        var result = await template.RenderAsync(unit, MemberRenamer);
        
        Assert.That(result, Is.EqualTo($"test {unit.Name}"));
    }

    private string MemberRenamer(MemberInfo member)
    {
        return member.Name;
    }
}