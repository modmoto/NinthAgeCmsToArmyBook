using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NinthAgeCmsToArmyBook.Shared.ArmyBooks;
using Scriban;
using Scriban.Runtime;

namespace NinthAgeCmsToArmyBook.Api.Latex;

public class TexTransformer
{
    public const string BaseFolderTemplates = "Latex/TemplateFiles";
    public const string BaseFolderTexFiles = "TexFiles";
    public const string BaseFolderPdfFiles = "PdfFiles";
    
    private readonly LatexConfiguration _latexConfiguration;

    public TexTransformer(LatexConfiguration latexConfiguration)
    {
        _latexConfiguration = latexConfiguration;
    }
    
    public async Task CreateTexFile(string armyName, ArmyBook selectedBook)
    {
        var unitContent = string.Empty;
        foreach (var selectedBookUnit in selectedBook.Units)
        {
            var unitEntry = await RenderTemplate("UnitEntry", selectedBookUnit);
            unitContent += unitEntry;
        }
        
        var bodyContentRender = await RenderTemplate("MainDocument", new { ArmyName = armyName, selectedBook.Version, UnitContent = unitContent});

        var texFilePath = GetTexFilePath(armyName, selectedBook.Version);
        var directoryInfo = new FileInfo(texFilePath).Directory;
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }
        if (!File.Exists(texFilePath))
        {
            var handle = File.Create(texFilePath);
            await handle.FlushAsync();
            await handle.DisposeAsync();
        }
        await File.WriteAllTextAsync(texFilePath, bodyContentRender);
    }
    
    public async Task CreatePdf(string armyName, string version)
    {
        var texFilePath = GetTexFilePath(armyName, version);
        var pdfPathParts = GetPdfFilePath(armyName, version).Split("/").SkipLast(1);
        var basePath = string.Join("//", pdfPathParts);
        var process = Process.Start(
            new ProcessStartInfo
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = _latexConfiguration.LatexExecutablePath,
                ArgumentList = { $"--output-directory={basePath}", texFilePath }
            });
        await process.WaitForExitAsync();
    }

    private async Task<string> RenderTemplate(string templateName, dynamic content)
    {
        var filePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{BaseFolderTemplates}/{templateName}.tex";
        var readAllTextAsync = await File.ReadAllTextAsync(filePath);
        var template = Template.Parse(readAllTextAsync);
        return await template.RenderAsync(content, (MemberRenamerDelegate)(member => member.Name));
    }
    
    public string GetFilePath(string armyName, string version, string baseFolder, string suffix)
    {
        var filePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{baseFolder}/{armyName.Replace(" ", "_")}/{version}/{armyName.Replace(" ", "_")}{version}.{suffix}";
        return filePath;
    }

    private string GetTexFilePath(string armyName, string version)
    {
        return GetFilePath(armyName, version, BaseFolderTexFiles, "tex");
    }
    
    private string GetPdfFilePath(string armyName, string version)
    {
        return GetFilePath(armyName, version, BaseFolderPdfFiles, "pdf");
    }
    
    public async Task<byte[]> GetPdfFile(string armyName, string version)
    {
        return await File.ReadAllBytesAsync(GetPdfFilePath(armyName, version));
    }
}