using System.Diagnostics;
using System.Linq;

namespace NinthAgeCmsToArmyBook.Shared.Latex;

public class LatexRepository
{
    private readonly LatexConfiguration _latexConfiguration;

    public LatexRepository(LatexConfiguration latexConfiguration)
    {
        _latexConfiguration = latexConfiguration;
    }
    
    public void CreatePdf(string texFilePath)
    {
        var pathParts = texFilePath.Split("/").SkipLast(1);
        var basePath = string.Join("/", pathParts);
        var process = Process.Start(
            new ProcessStartInfo
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = _latexConfiguration.LatexExecutablePath,
                ArgumentList = { $"--output-directory={basePath}", texFilePath }
            });
        process.WaitForExit();
    }
}

public class LatexConfiguration
{
    public LatexConfiguration(string latexExecutablePath)
    {
        LatexExecutablePath = latexExecutablePath;
    }
    public string LatexExecutablePath { get; }
}