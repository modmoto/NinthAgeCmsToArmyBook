using System.Diagnostics;

namespace NinthAgeCmsToArmyBook.Latex;

public class LatexRepository
{
    public void CreateLatex(string texFilePath)
    {
        var pathParts = texFilePath.Split("/").SkipLast(1);
        var basePath = string.Join("/", pathParts);
        var process = Process.Start(
            new ProcessStartInfo
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = "/usr/bin/pdflatex",
                // FileName = "/Library/TeX/texbin/pdflatex",
                ArgumentList = { $"--output-directory={basePath}", texFilePath }
            });
        process.WaitForExit();
    }
}