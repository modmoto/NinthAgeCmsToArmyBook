using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NinthAgeCmsToArmyBook.Shared.ArmyBooks;
using Scriban;
using Scriban.Runtime;

namespace NinthAgeCmsToArmyBook.Api.Latex;

public class TexTransformer
{
    
    public const string BaseFolder = "Latex/TemplateFiles";
    public async Task CreateTexFile(string armyName, ArmyBook selectedBook, string targetPath)
    {
        var unitContent = string.Empty;
        foreach (var selectedBookUnit in selectedBook.Units)
        {
            var unitEntry = await RenderTemplate("UnitEntry", selectedBookUnit);
            unitContent += unitEntry;
        }
        
        var bodyContentRender = await RenderTemplate("MainDocument", new { ArmyName = armyName, selectedBook.Version, UnitContent = unitContent});

        if (!File.Exists(targetPath))
        {
            var handle = File.Create(targetPath);
            await handle.FlushAsync();
            await handle.DisposeAsync();
        }
        await File.WriteAllTextAsync(targetPath, bodyContentRender);
    }

    private async Task<string> RenderTemplate(string templateName, dynamic content)
    {
        var filePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{BaseFolder}/{templateName}.tex";
        var readAllTextAsync = await File.ReadAllTextAsync(filePath);
        var template = Template.Parse(readAllTextAsync);
        return await template.RenderAsync(content, (MemberRenamerDelegate)(member => member.Name));
    }
}