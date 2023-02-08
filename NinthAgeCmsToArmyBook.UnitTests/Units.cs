using NinthAgeCmsToArmyBook.ArmyBooks;
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
}