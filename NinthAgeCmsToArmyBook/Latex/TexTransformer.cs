using System.Reflection;
using NinthAgeCmsToArmyBook.ArmyBooks;
using Scriban;

namespace NinthAgeCmsToArmyBook.Latex;

public class TexTransformer
{
    
    public const string BaseFolder = "Latex/TemplateFiles";
    public async Task CreateTexFile(string armyName, ArmyBook selectedBook, string targetPath)
    {
        var content = String.Empty;
        foreach (var selectedBookUnit in selectedBook.Units)
        {
            var unitEntry = await RenderTemplate("UnitEntry", selectedBookUnit);
            content += unitEntry;
        }
        
        var bodyContentRender = await RenderTemplate("MainDocument", new { armyName, selectedBook.Version, content});

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
        return await template.RenderAsync(content);
    }
}